using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings.Events;

public record BookingUpdatedDomainEvent(Guid BookingId) : IDomainEvent
{
    
};