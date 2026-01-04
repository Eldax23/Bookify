using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings.Events;

public record BookingCompletedDomainEvent(Guid BookingId) : IDomainEvent
{
    
}

public record BookingCancelledDomainEvent(Guid BookingId) : IDomainEvent
{
    
}