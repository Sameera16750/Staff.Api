using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Staff.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RequiredintUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deductions",
                table: "Payroll");

            migrationBuilder.AddColumn<long>(
                name: "PayrollId",
                table: "Deductions",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deductions_PayrollId",
                table: "Deductions",
                column: "PayrollId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deductions_Payroll_PayrollId",
                table: "Deductions",
                column: "PayrollId",
                principalTable: "Payroll",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deductions_Payroll_PayrollId",
                table: "Deductions");

            migrationBuilder.DropIndex(
                name: "IX_Deductions_PayrollId",
                table: "Deductions");

            migrationBuilder.DropColumn(
                name: "PayrollId",
                table: "Deductions");

            migrationBuilder.AddColumn<List<int>>(
                name: "Deductions",
                table: "Payroll",
                type: "integer[]",
                nullable: false);
        }
    }
}
