using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings.Events;

public record BookingRescheduledDomainEvent(Guid BookingId) : IDomainEvent
{
    
};