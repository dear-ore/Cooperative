using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cooperative.Migrations
{
    /// <inheritdoc />
    public partial class AddContributionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "SavingsBalance",
                table: "Cooperators",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Contributions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CooperatorId = table.Column<int>(type: "int", nullable: false),
                    SavingsAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InvestmentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SharesAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BuildingFundAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DateRecorded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contributions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contributions");

            migrationBuilder.DropColumn(
                name: "SavingsBalance",
                table: "Cooperators");
        }
    }
}
