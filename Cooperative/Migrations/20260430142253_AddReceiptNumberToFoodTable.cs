using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cooperative.Migrations
{
    /// <inheritdoc />
    public partial class AddReceiptNumberToFoodTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReceiptNumber",
                table: "Food",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiptNumber",
                table: "Food");
        }
    }
}
