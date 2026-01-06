using System.Data;
using Bookify.Application.Abstractions.Data;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Bookings.Enums;
using Dapper;

namespace Bookify.Application.Apartments.SearchApartments;

public class SearchApartmentsQueryHandler : IQueryHandler<SearchApartmentsQuery , IReadOnlyList<ApartmentResponse>>
{
    private static readonly int[] ActiveBookingStatuses =
    {
        (int)BookingStatus.Reserved,
        (int)BookingStatus.Confirmed,
        (int)BookingStatus.Completed,
        (int)BookingStatus.Cancelled,
    };
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    public async Task<Result<IReadOnlyList<ApartmentResponse>>> Handle(SearchApartmentsQuery request, CancellationToken cancellationToken)
    {
        if (request.StartDate > request.EndDate)
            return new List<ApartmentResponse>();

        using var connection = _sqlConnectionFactory.CreateConnection();


        const string sql = """
                           SELECT * FROM apartments as a
                           WHERE NOT EXISTS(
                               SELECT 1 FROM bookings AS b
                               WHERE
                                   b.apartment_id = a.id AND
                                   b.duration_start >= @StartDate AND
                                   b.duration_end <= @EndDate AND
                                   b.status = ANY(@ActiveBookingStatuses)
                           )
                           """;

        var apartments = await connection.QueryAsync<ApartmentResponse , AddressResponse , ApartmentResponse>(
            sql ,
            (apartment, address) =>
            {
                apartment.Address = address;
                return apartment;
            },
            new
            {
                request.StartDate,
                request.EndDate,
                ActiveBookingStatuses
            } , splitOn:"Country");


        return apartments.ToList();
    }
}