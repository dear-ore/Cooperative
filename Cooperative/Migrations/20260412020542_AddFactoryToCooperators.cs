using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cooperative.Migrations
{
    /// <inheritdoc />
    public partial class AddFactoryToCooperators : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Factory",
                table: "Cooperators",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Factory",
                table: "Cooperators");
        }
    }
}
