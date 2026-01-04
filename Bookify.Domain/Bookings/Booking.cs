using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings.Enums;
using Bookify.Domain.Bookings.Events;
using Bookify.Domain.Bookings.Records;
using Bookify.Domain.Bookings.Services;
using Bookify.Domain.Shared.Records;

namespace Bookify.Domain.Bookings;

// the process: Reserve -> Confirm -> Complete

public sealed class Booking : Entity
{
    public Booking(Guid id, Guid userId, Money priceForPeriod, Money cleaningFee, Money amentiesFee,
        Money totalPrice, BookingStatus status, DateRange duration, DateTime createdOnUtc ) : base(id)
    {
        UserId = userId;
        PriceForPeriod = priceForPeriod;
        CleaningFee = cleaningFee;
        AmentiesFee = amentiesFee;
        TotalPrice = totalPrice;
        Status = status;
        Duration = duration;
        CreatedOnUtc = createdOnUtc;
    }

    public Guid ApartmentId { get; private set; }
    public Guid UserId { get; private set; }
    public Money PriceForPeriod { get; private set; }
    public Money CleaningFee { get; private set; }
    public Money AmentiesFee { get; private set; }
    public Money TotalPrice { get; private set; }
    public BookingStatus Status { get; private set; }
    public DateRange Duration { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }
    public DateTime? ConfirmedOnUtc { get; private set; }
    public DateTime? RejectedOnUtc { get; private set; }
    public DateTime? CompletedOnUtc { get; private set; }
    public DateTime? CancelledOnUtc { get; private set; }

    public static Booking Reserve(Apartment apartment, Guid userId, DateRange duration, DateTime utcNow,
        PricingService pricingService)
    {
        PricingDetails pricingDetails = pricingService.CalculatePrice(apartment, duration);
        Booking booking = new Booking(Guid.NewGuid(), userId, pricingDetails.priceForPeriod, pricingDetails.cleaningFee,
            pricingDetails.amentiesCharge, pricingDetails.totalPrice, BookingStatus.Reserved, duration, utcNow);
        
        
        booking.RaiseDomainEvent(new BookingReservedDomainEvent(booking.Id));
        apartment.LastBookedOnUtc = utcNow;
        return booking;
        
    }

    public Result Confirm(DateTime utcNow)
    {
        // if the date is not reserved then that would be a problem (GIVEN THE HIERARCHY ABOVE)
        if (Status != BookingStatus.Reserved)
        {
            return Result.Failure(BookingErrors.NotReserved);
        }

        Status = BookingStatus.Confirmed;
        ConfirmedOnUtc = utcNow;
        RaiseDomainEvent(new BookingConfirmedDomainEvent(Id));
        return Result.Success();
    }

    public Result Reject(DateTime utcNow)
    {
        if (Status != BookingStatus.Reserved)
        {
            return Result.Failure(BookingErrors.NotPending);
        }

        Status = BookingStatus.Rejected;
        RejectedOnUtc = utcNow;
        RaiseDomainEvent(new BookingRejectedDomainEvent(Id));
        return Result.Success();
    }

    public Result Complete(DateTime utcNow)
    {
        
        // if the booking is not confirmed then it cannot be completed
        if (Status != BookingStatus.Confirmed)
        {
            return Result.Failure(BookingErrors.NotConfirmed);
        }

        Status = BookingStatus.Completed;
        CompletedOnUtc = utcNow;
        RaiseDomainEvent(new BookingCompletedDomainEvent(Id));
        return Result.Success();
    }

    public Result Cancel(DateTime utcNow)
    {
        if (Status != BookingStatus.Confirmed)
        {
            return Result.Failure(BookingErrors.NotConfirmed);
        }
        
        DateOnly currDate = DateOnly.FromDateTime(utcNow);

        // aka if the customer tried to cancel the reservation after the reservation started
        if (Duration.Start > currDate)
        {
            return Result.Failure(BookingErrors.AlreadyStarted);
        }

        CancelledOnUtc = utcNow;
        RaiseDomainEvent(new BookingCancelledDomainEvent(Id));
        return Result.Success();
    }
}