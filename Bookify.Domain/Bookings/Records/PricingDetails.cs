
using Bookify.Domain.Shared.Records;

namespace Bookify.Domain.Bookings.Records;

public record PricingDetails(Money priceForPeriod, Money cleaningFee, Money amentiesCharge, Money totalPrice);
