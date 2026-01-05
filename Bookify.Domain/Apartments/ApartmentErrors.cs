using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Apartments;

public class ApartmentErrors
{
    public static Error NotFound = new Error("Apartment.NotFound", "This Apartment Cannot be found");
}