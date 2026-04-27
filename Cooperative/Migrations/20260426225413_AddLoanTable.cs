using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cooperative.Migrations
{
    /// <inheritdoc />
    public partial class AddLoanTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfEmployment",
                table: "Cooperators",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Cooperators",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsMemberOfSimilarSociety",
                table: "Cooperators",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MaritalStatus",
                table: "Cooperators",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "MembershipCommencementDate",
                table: "Cooperators",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "MonthlyContribution",
                table: "Cooperators",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PostHeld",
                table: "Cooperators",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SimilarSocietyDetails",
                table: "Cooperators",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Cooperators",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CooperatorId = table.Column<int>(type: "int", nullable: false),
                    PrincipalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalRepayable = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyInstallment = table.Column<int>(type: "int", nullable: false),
                    DateTaken = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InstallmentsRemaining = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropColumn(
                name: "DateOfEmployment",
                table: "Cooperators");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "Cooperators");

            migrationBuilder.DropColumn(
                name: "IsMemberOfSimilarSociety",
                table: "Cooperators");

            migrationBuilder.DropColumn(
                name: "MaritalStatus",
                table: "Cooperators");

            migrationBuilder.DropColumn(
                name: "MembershipCommencementDate",
                table: "Cooperators");

            migrationBuilder.DropColumn(
                name: "MonthlyContribution",
                table: "Cooperators");

            migrationBuilder.DropColumn(
                name: "PostHeld",
                table: "Cooperators");

            migrationBuilder.DropColumn(
                name: "SimilarSocietyDetails",
                table: "Cooperators");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Cooperators");
        }
    }
}
