using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cooperative.Migrations
{
    /// <inheritdoc />
    public partial class CHangeCoopNumberToInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CooperativeId",
                table: "Repayments");

            migrationBuilder.RenameColumn(
                name: "MyProperty",
                table: "Repayments",
                newName: "CooperatorId");

            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "Souvenirs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "StaffNumber",
                table: "Cooperators",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "CoopNumber",
                table: "Cooperators",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Souvenirs");

            migrationBuilder.RenameColumn(
                name: "CooperatorId",
                table: "Repayments",
                newName: "MyProperty");

            migrationBuilder.AddColumn<int>(
                name: "CooperativeId",
                table: "Repayments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "StaffNumber",
                table: "Cooperators",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CoopNumber",
                table: "Cooperators",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
