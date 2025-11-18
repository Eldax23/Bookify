using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments.Records;
using Bookify.Domain.Bookings.Enums;
using Bookify.Domain.Bookings.Records;

namespace Bookify.Domain.Bookings;

public sealed class Booking : Entity
{
    public Booking(Guid id, Guid userId, Money priceForPeriod, Money cleaningFee, Money amentiesFee,
        Money totalPrice, BookingStatus status, DateRange duration, DateTime createdOnUtc
    ) : base(id)
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
    
    
}