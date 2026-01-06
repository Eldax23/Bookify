using Bookify.Domain.Apartments.Records;

namespace Bookify.Application.Apartments.SearchApartments;

public class ApartmentResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; }
    public AddressResponse Address { get; set; }
}