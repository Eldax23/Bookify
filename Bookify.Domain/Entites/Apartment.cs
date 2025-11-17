using Bookify.Domain.Abstractions;
using Bookify.Domain.Entites.Enums;

namespace Bookify.Domain.Entites;

public sealed class Apartment : Entity
{
    public Apartment(Guid id) : base(id)
    {
        
    }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Country { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public decimal PriceAmount { get; set; }
    public string PriceCurrency { get; set; }
    public decimal CleaningFeeAmount { get; set; }
    public string CleaningFeeCurrency { get; set; }
    public DateTime? LastBookedOnUtc { get; set; }
    public List<Amenity> Amenities { get; set; } = new();
}