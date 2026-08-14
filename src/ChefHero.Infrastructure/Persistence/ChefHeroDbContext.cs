using ChefHero.Domain.User;

using BringableKitchenItemEntity =
    ChefHero.Domain.BringableKitchenItem.BringableKitchenItem;

using Microsoft.EntityFrameworkCore;

namespace ChefHero.Infrastructure.Persistence;

public class ChefHeroDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<BringableKitchenItemEntity> BringableKitchenItems { get; set; }

    public ChefHeroDbContext(
        DbContextOptions<ChefHeroDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ChefHeroDbContext).Assembly);
    }
}