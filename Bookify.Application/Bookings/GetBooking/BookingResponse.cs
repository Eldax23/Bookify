namespace Bookify.Application.Bookings.GetBooking;

public class BookingResponse
{
    public Guid Id { get; private set; }
    public Guid ApartmentId { get; private set; }
    public Guid UserId { get; private set; }
    public decimal PriceAmount { get; private set; }
    public string PriceCurrency { get; private set; }
    public decimal CleaningFeeAmount { get; private set; }
    public string CleaningFeeCurrency { get; private set; }
    public decimal AmentiesUpCharge { get; private set; }
    public string AmentiesUpChargeCurrency { get; private set; }
    public decimal TotalPriceAmount { get; private set; }
    public string TotalPriceCurrency { get; private set; }
    public int Status { get; private set; }
    public DateOnly DurationStart { get; private set; }
    public DateOnly DurationEnd { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }
    
}