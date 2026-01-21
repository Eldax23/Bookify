using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Bookify.Domain.Bookings.Enums;
using Bookify.Domain.Bookings.Records;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure.Repositories;

internal sealed class BookingRepository : Repository<Booking> , IBookingRepository
{
    private static readonly BookingStatus[] ActiveBookingStatuses =
    {
        BookingStatus.Reserved,
        BookingStatus.Confirmed,
        BookingStatus.Completed,
    };
    
    public BookingRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }


    public async Task<bool> IsOverlappingAsync(Apartment apartment, DateRange duration, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<Booking>()
            .AnyAsync(b => b.ApartmentId == apartment.Id &&
                           b.Duration.Start >= duration.End &&
                           b.Duration.End >= duration.Start &&
                           ActiveBookingStatuses.Contains(b.Status)
             , cancellationToken);
    }
}