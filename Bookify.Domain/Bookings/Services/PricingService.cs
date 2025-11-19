using Bookify.Domain.Apartments;
using Bookify.Domain.Apartments.Enums;
using Bookify.Domain.Shared.Records;
using Bookify.Domain.Bookings.Records;

namespace Bookify.Domain.Bookings.Services;

public class PricingService
{
    public PricingDetails CalculatePrice(Apartment apartment, DateRange period)
    {
        Currency currency = apartment.Price.currency;

        Money priceForPeriod = new Money(apartment.Price.Amount * period.LengthInDays, currency);

        decimal percentageCharge = 0;
        foreach (Amenity amenity in apartment.Amenities)
        {
            percentageCharge += amenity switch
            {
                Amenity.GardenView or Amenity.MountainView => 0.05m,
                Amenity.AirConditioning => 0.01m,
                Amenity.Parking => 0.01m,
                _ => 0
            };
        }

        Money amentiesUpCharge = Money.Zero();
        if (percentageCharge > 0)
        {
            amentiesUpCharge = new Money(percentageCharge * priceForPeriod.Amount ,  currency);
        }

        Money totalPrice = priceForPeriod;

        if (!apartment.CleaningFee.IsZero())
        {
            totalPrice += apartment.CleaningFee;           
        }

        totalPrice += amentiesUpCharge;
        
        return new PricingDetails(priceForPeriod , apartment.CleaningFee , amentiesUpCharge ,  totalPrice);
    }
}