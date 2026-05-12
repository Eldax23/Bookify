using Bookify.Application.Abstractions.Data;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Application.Bookings.GetBooking;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Bookings;
using Dapper;

namespace Bookify.Application.Bookings.GetAllBookings;

public class GetAllQueryHandler : IQueryHandler<GetAllQuery , List<BookingResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    public GetAllQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }
    public async Task<Result<List<BookingResponse>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        
        using var connnection = _sqlConnectionFactory.CreateConnection();
        var query = """
                    SELECT 
                        id AS Id,
                        apartment_id AS ApartmentId,
                        user_id AS UserId,
                        price_for_period_amount AS PriceAmount,
                        price_for_period_currency AS PriceCurrency,
                        cleaning_fee_amount AS CleaningFeeAmount,
                        cleaning_fee_currency AS CleaningFeeCurrency,
                        amenties_fee_amount AS AmentiesUpCharge,
                        amenties_fee_currency AS AmentiesUpChargeCurrency,
                        total_price_amount AS TotalPriceAmount,
                        total_price_currency AS TotalPriceCurrency,
                        status AS Status,
                        duration_start AS DurationStart,
                        duration_end AS DurationEnd,
                        created_on_utc AS CreatedOnUtc
                    FROM bookings
                    """;

        var bookings = (await connnection.QueryAsync<BookingResponse>(query)).ToList();

        if (bookings.Count == 0)
        {
            return Result.Failure<List<BookingResponse>>(BookingErrors.NotFound);
        }
        
        
        return Result.Success(bookings);
    }
}