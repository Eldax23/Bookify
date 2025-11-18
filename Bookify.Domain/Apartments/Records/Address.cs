namespace Bookify.Domain.Apartments.Records;

public record Address(
    string Country,
    string State,
    string ZipCode,
    string City,
    string Street
);