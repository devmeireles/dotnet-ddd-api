using ChefHero.Domain.User;

using Microsoft.EntityFrameworkCore;

namespace ChefHero.Infrastructure.Persistence;

public class ChefHeroDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

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