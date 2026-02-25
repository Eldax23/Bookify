using Bookify.Application.Abstractions.Clock;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Application.Exceptions;
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
    private readonly PricingService _pricingService;
    private readonly IDateTimeProvider _dateTimeProvider;
    public ReserveBookingCommandHandler(IBookingRepository bookingRepository,
        IApartmentRepository apartmentRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork ,
        PricingService pricingService,
        IDateTimeProvider dateTimeProvider)
    {
        _bookingRepository = bookingRepository;
        _apartmentRepository = apartmentRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _pricingService = pricingService;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<Guid>> Handle(ReserveBookingCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(request.UserId , cancellationToken);

        if (user is null)
            return Result.Failure<Guid>(UserErrors.NotFound);

        Apartment? apartment = await _apartmentRepository.GetByIdAsync(request.ApartmentId , cancellationToken);
        if (apartment is null)
            return Result.Failure<Guid>(ApartmentErrors.NotFound);

        DateRange duration = DateRange.Create(request.StartDate, request.EndDate);

        if (await _bookingRepository.IsOverlappingAsync(apartment, duration, cancellationToken))
        {
            return Result.Failure<Guid>(BookingErrors.Overlap);
        }

        try
        {
            Booking? booking = Booking.Reserve(apartment,
                user.Id,
                duration,
                _dateTimeProvider.UtcNow,
                _pricingService);

            _bookingRepository.Add(booking);
            await _unitOfWork.SaveChangesAsync();

            return booking.Id;
        }
        catch (ConcurrencyException ex)
        {
            return Result.Failure<Guid>(BookingErrors.Overlap);
        }

    }
}
