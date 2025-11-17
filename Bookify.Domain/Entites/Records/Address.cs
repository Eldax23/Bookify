namespace Bookify.Domain.Entites.Records;

public record Address(
    string Country,
    string State,
    string ZipCode,
    string City,
    string Street
);