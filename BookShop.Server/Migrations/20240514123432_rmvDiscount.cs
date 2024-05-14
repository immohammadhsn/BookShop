using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShop.Server.Migrations
{
    /// <inheritdoc />
    public partial class rmvDiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Books");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Discount",
                table: "Books",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
