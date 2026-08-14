using ChefHero.Domain.User;

using Microsoft.EntityFrameworkCore;

namespace ChefHero.Infrastructure.Persistence;

public class ChefHeroDbContext : DbContext
{
    public ChefHeroDbContext(
        DbContextOptions<ChefHeroDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ChefHeroDbContext).Assembly);
    }
}