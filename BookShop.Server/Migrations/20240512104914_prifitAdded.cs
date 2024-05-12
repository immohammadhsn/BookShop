using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShop.Server.Migrations
{
    /// <inheritdoc />
    public partial class prifitAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Profit",
                table: "SoldBooks",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Profit",
                table: "SoldBooks");
        }
    }
}
