using System.Windows.Input;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Application.Exceptions;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Bookify.Domain.Bookings.Services;
using Bookify.Domain.Users;

namespace Bookify.Application.Bookings.RescheduleBookingCommand;

public sealed class ReschduleBookingCommandHandler : ICommandHandler<RescheduleBookingCommand , bool>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IApartmentRepository _apartmentRepository;
    private readonly PricingService _pricingService;
    public ReschduleBookingCommandHandler(IBookingRepository repository , IUnitOfWork unitOfWork ,
        IApartmentRepository apartmentRepository, PricingService  pricingService)
    {
        _bookingRepository = repository;
        _unitOfWork = unitOfWork;
        _apartmentRepository = apartmentRepository;
        _pricingService = pricingService;
    }
    public async Task<Result<bool>> Handle(RescheduleBookingCommand request, CancellationToken cancellationToken)
    {
        Booking? booking = await _bookingRepository.GetByIdAsync(request.Id , cancellationToken);
        Apartment? apartment = await _apartmentRepository.GetByIdAsync(request.ApartmentId , cancellationToken);
        if (apartment is null)
        {
            return Result.Failure<bool>(ApartmentErrors.NotFound);
        }
        if (booking is null)
        {
            return Result.Failure<bool>(BookingErrors.NotFound);
        }
        
        booking.Reschedule(apartment , request.StartDate ,  request.EndDate , _pricingService);
        // update the booking
        try
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            return Result.Failure<bool>(BookingErrors.NotFound);
        }

        
    }
}