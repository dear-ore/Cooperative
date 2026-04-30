using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cooperative.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactionTypeToLoanModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "MonthlyInstallment",
                table: "Loans",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "LoanTransactionType",
                table: "Loans",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoanTransactionType",
                table: "Loans");

            migrationBuilder.AlterColumn<int>(
                name: "MonthlyInstallment",
                table: "Loans",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
