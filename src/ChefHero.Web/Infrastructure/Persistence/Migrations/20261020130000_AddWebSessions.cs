using ChefHero.Web.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChefHero.Web.Infrastructure.Persistence.Migrations;

[DbContext(typeof(WebSessionDbContext))]
[Migration("20261020130000_AddWebSessions")]
public sealed class AddWebSessions : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "WebSessions",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                UserId = table.Column<Guid>(type: "uuid", nullable: false),
                ProtectedAccessToken = table.Column<string>(type: "text", nullable: false),
                CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                ExpiresAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                RevokedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_WebSessions", session => session.Id);
            });

        migrationBuilder.CreateIndex(
            name: "IX_WebSessions_ExpiresAt",
            table: "WebSessions",
            column: "ExpiresAt");

        migrationBuilder.CreateIndex(
            name: "IX_WebSessions_UserId",
            table: "WebSessions",
            column: "UserId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "WebSessions");
    }
}
