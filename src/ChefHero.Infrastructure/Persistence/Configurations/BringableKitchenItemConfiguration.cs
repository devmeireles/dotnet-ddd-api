using BringableKitchenItemEntity =
    ChefHero.Domain.BringableKitchenItem.BringableKitchenItem;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChefHero.Infrastructure.Persistence.Configurations;

public class BringableKitchenItemConfiguration
    : IEntityTypeConfiguration<BringableKitchenItemEntity>
{
    public void Configure(
        EntityTypeBuilder<BringableKitchenItemEntity> builder)
    {
        builder.HasKey(item => item.Id);

        builder.Property(item => item.Name)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(item => item.Description)
            .HasMaxLength(256);

        builder.Property(item => item.IsActive)
            .IsRequired();

        builder.HasIndex(item => item.Name)
            .IsUnique();
    }
}