using Bookify.Domain.Apartments;
using Bookify.Domain.Apartments.Records;
using Bookify.Domain.Shared.Records;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookify.Infrastructure.Configurations;

internal sealed class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
{
    public void Configure(EntityTypeBuilder<Apartment> builder)
    {
        builder.ToTable("Apartments");

        builder.HasKey(a => a.Id);
        builder.OwnsOne(a => a.Address);
        builder.Property(a => a.Name)
            .HasMaxLength(200)
            .HasConversion(name => name.Value , val => new Name(val));

        builder.Property(a => a.Description)
            .HasMaxLength(200)
            .HasConversion(desc => desc.Value , val => new Description(val));

        builder.OwnsOne(a => a.Price , priceBuilder =>
        {
            priceBuilder.Property(p => p.currency)
                .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
        });

        builder.OwnsOne(a => a.CleaningFee, cleanFeeBuilder =>
        {
            cleanFeeBuilder.Property(p => p.currency)
                .HasConversion(currency => currency.Code , code => Currency.FromCode(code));
        });

        builder.Property<uint>("Version").IsRowVersion();

    }
}