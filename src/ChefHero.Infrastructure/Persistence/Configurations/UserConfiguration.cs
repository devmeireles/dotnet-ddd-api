using ChefHero.Domain.User;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChefHero.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);

        builder.Property(user => user.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(user => user.PasswordHash)
            .IsRequired();

        builder.Property(user => user.Role)
            .IsRequired();

        builder.Property(user => user.Phone)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(user => user.AddressLine)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(user => user.City)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(user => user.State)
            .IsRequired()
            .HasMaxLength(2);

        builder.Property(user => user.ZipCode)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(user => user.IsActive)
            .IsRequired();

        builder.Property(user => user.Email)
            .HasConversion(
                email => email.Value,
                value => Email.Create(value))
            .HasColumnName("Email")
            .IsRequired()
            .HasMaxLength(320);

        builder.HasIndex(user => user.Email)
            .IsUnique();
    }
}