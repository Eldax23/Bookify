using Bookify.Application.Abstractions.Clock;
using Bookify.Application.Abstractions.Email;
using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Bookify.Domain.Bookings.Events;
using Bookify.Domain.Users;
using MediatR;

namespace Bookify.Application.Bookings.ReserveBooking;

public class BookingReservedDomainEventHandler : INotificationHandler<BookingReservedDomainEvent>
{
    private readonly IUserRepository _userRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly IEmailService _emailService;
    public async Task Handle(BookingReservedDomainEvent notification, CancellationToken cancellationToken)
    {
        Booking? booking = await _bookingRepository.GetByIdAsync(notification.BookingId , cancellationToken);

        if (booking is null)
            return;
        
        User? user = await _userRepository.GetByIdAsync(booking.UserId , cancellationToken);
        
        if (user is null)
            return;

        await _emailService.SendAsync(user.Email, "Booking reserved", "you must confirm this booking");

    }
}