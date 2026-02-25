using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Bookify.Domain.Shared.Records;
using Bookify.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookify.Infrastructure.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("bookings");
        builder.HasKey(b => b.Id);

        builder.OwnsOne(p => p.PriceForPeriod , priceBuilder =>
        {
            priceBuilder.Property(m => m.currency)
                .HasConversion(c => c.Code, val => Currency.FromCode(val));
        });

        builder.OwnsOne(b => b.CleaningFee, priceBuilder =>
        {
            priceBuilder.Property(m => m.currency)
                .HasConversion(c => c.Code, val => Currency.FromCode(val));
        });

        builder.OwnsOne(b => b.AmentiesFee, priceBuilder =>
        {
            priceBuilder.Property(m => m.currency)
                .HasConversion(c => c.Code , val => Currency.FromCode(val));
        });

        builder.OwnsOne(b => b.TotalPrice, priceBuilder =>
        {
            priceBuilder.Property(m => m.currency)
                .HasConversion(c => c.Code, val => Currency.FromCode(val));
        });

        builder.OwnsOne(b => b.Duration);
        
        
        // Apartment -> Booking (One-To-Many)

        builder.HasOne<Apartment>()
            .WithMany()
            .HasForeignKey(b => b.ApartmentId);
        
        // User -> Booking (One-To-Many)
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(b => b.UserId);
    }
}