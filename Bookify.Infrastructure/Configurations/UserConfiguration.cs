using Bookify.Domain.Users;
using Bookify.Domain.Users.Records;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookify.Infrastructure.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(u => u.Id);

        builder.Property(u => u.FirstName)
            .HasMaxLength(200)
            .HasConversion(firstname => firstname.Value, val => new FirstName(val));
        
        builder.Property(u => u.LastName)
            .HasMaxLength(200)
            .HasConversion(lastname => lastname.Value, val => new LastName(val));

        builder.Property(u => u.Email)
            .HasMaxLength(400)
            .HasConversion(email => email.Value, val => new Domain.Users.Records.Email(val));
        
        builder.HasIndex(u => u.Email)
            .IsUnique();
        
        builder.HasIndex(u => u.IdentityId).IsUnique();
    }
}