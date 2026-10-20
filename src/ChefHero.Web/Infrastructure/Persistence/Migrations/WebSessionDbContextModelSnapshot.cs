using ChefHero.Web.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ChefHero.Web.Infrastructure.Persistence.Migrations;

[DbContext(typeof(WebSessionDbContext))]
public sealed class WebSessionDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasAnnotation("ProductVersion", "10.0.11")
            .HasAnnotation("Relational:MaxIdentifierLength", 63);

        modelBuilder.Entity<WebSession>(entity =>
        {
            entity.ToTable("WebSessions");

            entity.HasKey(session => session.Id);

            entity.HasIndex(session => session.ExpiresAt);
            entity.HasIndex(session => session.UserId);

            entity.Property(session => session.Id)
                .HasColumnType("uuid");

            entity.Property(session => session.UserId)
                .HasColumnType("uuid");

            entity.Property(session => session.ProtectedAccessToken)
                .IsRequired()
                .HasColumnType("text");

            entity.Property(session => session.CreatedAt)
                .HasColumnType("timestamp with time zone");

            entity.Property(session => session.ExpiresAt)
                .HasColumnType("timestamp with time zone");

            entity.Property(session => session.RevokedAt)
                .HasColumnType("timestamp with time zone");
        });
    }
}
