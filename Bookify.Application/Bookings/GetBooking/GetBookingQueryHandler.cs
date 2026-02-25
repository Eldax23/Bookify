using Bookify.Application.Abstractions.Data;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Bookings;
using Dapper;

namespace Bookify.Application.Bookings.GetBooking;

public class GetBookingQueryHandler : IQueryHandler<GetBookingQuery , BookingResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetBookingQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<BookingResponse>> Handle(GetBookingQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string query = """
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
                             WHERE id = @bookingId
                             """;
        BookingResponse? booking = await connection.QueryFirstOrDefaultAsync<BookingResponse>(
            query,
            new
            {
                bookingId = request.BookingId
            }
        );

        if (booking is null)
        {
            return Result.Failure<BookingResponse>(BookingErrors.NotFound);
        }
        
        return Result.Success(booking);
    }
}