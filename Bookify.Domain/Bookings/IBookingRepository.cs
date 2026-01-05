using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings.Records;

namespace Bookify.Domain.Bookings;

public interface IBookingRepository
{
    Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task Add(Booking booking);
    Task<bool> IsOverlappingAsync(Apartment apartment , DateRange duration , CancellationToken cancellationToken);
}