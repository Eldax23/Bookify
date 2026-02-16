using Bogus;
using Bookify.Application.Abstractions.Data;
using Bookify.Domain.Apartments;
using Bookify.Domain.Apartments.Enums;
using Bookify.Domain.Apartments.Records;
using Bookify.Domain.Shared.Records;
using Bookify.Infrastructure.Data;
using Dapper;

namespace Bookify.API.Extensions;

public static class SeedDataExtensions
{
    public static void SeedData(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();

        using var connection = sqlConnectionFactory.CreateConnection();
        Faker faker = new Faker();
        

        List<object> apartments = new();
        for (int i = 0; i < 100; i++)
        {
            apartments.Add(new
            {
                Id = Guid.NewGuid(),
                Name = faker.Name.FirstName(),
                Description = faker.Name.LastName(),
                Country = faker.Address.Country(),
                State = faker.Address.State(),
                City = faker.Address.City(),
                ZipCode = faker.Address.ZipCode(),
                Street = faker.Address.StreetAddress(),
                PriceAmount = faker.Random.Decimal(50 , 10000),
                PriceCurrency = "USD",
                CleaningFeeAmount = faker.Random.Decimal(50 , 200),
                CleaningFeeCurrency = "USD",
                Amenties = new List<int>()
                {
                    (int)Amenity.Parking,
                    (int)Amenity.MountainView
                }
            });
        }

        const string sql = """
                           INSERT INTO public."Apartments"
                           (id , name , description , address_country , address_state , address_zip_code , address_city,
                           address_street , price_amount , price_currency , cleaning_fee_amount , cleaning_fee_currency , amenities)
                           VALUES(@Id , @Name , @Description , @Country , @State , @ZipCode , @City ,  @Street , @PriceAmount , @PriceCurrency,
                           @CleaningFeeAmount , @CleaningFeeCurrency, @Amenties)
                           """;

        connection.Execute(sql, apartments);
    }
}