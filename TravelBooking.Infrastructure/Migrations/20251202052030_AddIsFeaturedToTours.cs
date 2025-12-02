using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsFeaturedToTours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                table: "Tours",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFeatured",
                table: "Tours");
        }
    }
}
