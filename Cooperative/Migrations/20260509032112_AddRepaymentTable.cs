using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cooperative.Migrations
{
    /// <inheritdoc />
    public partial class AddRepaymentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstallmentsRemaining",
                table: "Loans");

            migrationBuilder.CreateTable(
                name: "Repayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CooperativeId = table.Column<int>(type: "int", nullable: false),
                    ReceiptNumber = table.Column<int>(type: "int", nullable: false),
                    MyProperty = table.Column<int>(type: "int", nullable: false),
                    DeductionMethod = table.Column<int>(type: "int", nullable: false),
                    DateOfRepayment = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoanRepaymentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FoodRepaymentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SouvenirRepaymentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repayments", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Repayments");

            migrationBuilder.AddColumn<int>(
                name: "InstallmentsRemaining",
                table: "Loans",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
