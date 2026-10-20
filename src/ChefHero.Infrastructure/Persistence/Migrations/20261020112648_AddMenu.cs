using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChefHero.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMenu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BringableKitchenItems_Name",
                table: "BringableKitchenItems");

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    MinPeople = table.Column<int>(type: "integer", nullable: false),
                    MaxPeople = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BringableKitchenItems_Name",
                table: "BringableKitchenItems",
                column: "Name",
                unique: true,
                filter: "\"IsActive\" = true");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropIndex(
                name: "IX_BringableKitchenItems_Name",
                table: "BringableKitchenItems");

            migrationBuilder.CreateIndex(
                name: "IX_BringableKitchenItems_Name",
                table: "BringableKitchenItems",
                column: "Name",
                unique: true);
        }
    }
}
