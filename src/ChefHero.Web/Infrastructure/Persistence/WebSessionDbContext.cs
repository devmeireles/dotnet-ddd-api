using Microsoft.EntityFrameworkCore;

namespace ChefHero.Web.Infrastructure.Persistence;

public sealed class WebSessionDbContext(
    DbContextOptions<WebSessionDbContext> options) : DbContext(options)
{
    public DbSet<WebSession> Sessions => Set<WebSession>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WebSession>(entity =>
        {
            entity.ToTable("WebSessions");
            entity.HasKey(session => session.Id);
            entity.Property(session => session.ProtectedAccessToken)
                .IsRequired();
            entity.HasIndex(session => session.ExpiresAt);
            entity.HasIndex(session => session.UserId);
        });
    }
}
