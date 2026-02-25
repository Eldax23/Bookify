namespace Bookify.Application.Bookings.GetBooking;

public class BookingResponse
{
    public Guid Id { get;  set; }
    public Guid ApartmentId { get;  set; }
    public Guid UserId { get;  set; }
    public decimal PriceAmount { get;  set; }
    public string PriceCurrency { get;  set; }
    public decimal CleaningFeeAmount { get;  set; }
    public string CleaningFeeCurrency { get;  set; }
    public decimal AmentiesUpCharge { get;  set; }
    public string AmentiesUpChargeCurrency { get;  set; }
    public decimal TotalPriceAmount { get;  set; }
    public string TotalPriceCurrency { get;  set; }
    public int Status { get;  set; }
    public DateOnly DurationStart { get;  set; }
    public DateOnly DurationEnd { get;  set; }
    public DateTime CreatedOnUtc { get;  set; }

    private BookingResponse()
    {
        
    }

    public BookingResponse(Guid id , Guid apartmentId , Guid userId , 
        decimal priceAmount, string priceCurrency, decimal cleaningFeeAmount, string cleaningFeeCurrency,
        decimal amentiesUpCharge, string amentiesUpChargeCurrency,
        decimal totalPriceAmount, string totalPriceCurrency
        , DateOnly durationStart, DateOnly durationEnd, DateTime createdOnUtc)
    {
        Id = id;
        ApartmentId = apartmentId;
        UserId = userId;
        PriceAmount = priceAmount;
        PriceCurrency = priceCurrency;
        CleaningFeeAmount = cleaningFeeAmount;
        CleaningFeeCurrency = cleaningFeeCurrency;
        AmentiesUpCharge = amentiesUpCharge;
        AmentiesUpChargeCurrency = amentiesUpChargeCurrency;
        TotalPriceAmount = totalPriceAmount;
        TotalPriceCurrency = totalPriceCurrency;
        DurationStart = durationStart;
        DurationEnd = durationEnd;
        CreatedOnUtc = createdOnUtc;
    }
}