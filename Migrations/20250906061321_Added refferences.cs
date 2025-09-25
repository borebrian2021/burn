using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BURN_SOCIETY.Migrations
{
    /// <inheritdoc />
    public partial class Addedrefferences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Refference",
                table: "Registrations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Refference",
                table: "Registrations");
        }
    }
}
