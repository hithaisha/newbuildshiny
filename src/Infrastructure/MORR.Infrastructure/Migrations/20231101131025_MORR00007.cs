using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MORR.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MORR00007 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LoyalityPoints",
                table: "Product",
                newName: "LoyaltyPoints");

            migrationBuilder.AddColumn<decimal>(
                name: "LoyaltyPoints",
                table: "User",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoyaltyPoints",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "LoyaltyPoints",
                table: "Product",
                newName: "LoyalityPoints");
        }
    }
}
