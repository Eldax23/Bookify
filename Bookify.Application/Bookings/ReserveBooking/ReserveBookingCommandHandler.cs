using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Bookify.Domain.Bookings.Records;
using Bookify.Domain.Bookings.Services;
using Bookify.Domain.Users;

namespace Bookify.Application.Bookings.ReserveBooking;

public sealed class ReserveBookingCommandHandler : ICommandHandler<ReserveBookingCommand, Guid>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IApartmentRepository _apartmentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly PricingService pricingService;

    public ReserveBookingCommandHandler(IBookingRepository bookingRepository,
        IApartmentRepository apartmentRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _bookingRepository = bookingRepository;
        _apartmentRepository = apartmentRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(ReserveBookingCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(request.UserId);

        if (user is null)
            return Result.Failure<Guid>(UserErrors.NotFound);

        Apartment? apartment = await _apartmentRepository.GetByIdAsync(request.ApartmentId);
        if (apartment is null)
            return Result.Failure<Guid>(ApartmentErrors.NotFound);

        DateRange duration = DateRange.Create(request.StartDate, request.EndDate);
        
        Booking? booking = Booking.Reserve(apartment ,
            user.Id ,
            duration,
            DateTime.UtcNow,
            pricingService);

        await _bookingRepository.Add(booking);
        await _unitOfWork.SaveChangesAsync();

        return booking.Id;

    }
}
