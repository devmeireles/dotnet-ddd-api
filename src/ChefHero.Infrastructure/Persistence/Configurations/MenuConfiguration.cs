
using MenuEntity =
    ChefHero.Domain.Menu.Menu;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChefHero.Infrastructure.Persistence.Configurations;

public class MenuConfiguration : IEntityTypeConfiguration<MenuEntity>
{
    public void Configure(EntityTypeBuilder<MenuEntity> builder)
    {
        builder.ToTable("Menus");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Name)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(x => x.MinPeople)
            .IsRequired();

        builder.Property(x => x.MaxPeople)
            .IsRequired();

        builder.Property(x => x.Price)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired();
    }
}
