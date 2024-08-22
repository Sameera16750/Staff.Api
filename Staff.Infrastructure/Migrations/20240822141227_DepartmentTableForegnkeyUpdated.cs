using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Staff.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DepartmentTableForegnkeyUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_OrganizationDetails_OrganizationDetailsId",
                table: "Department");

            migrationBuilder.RenameColumn(
                name: "OrganizationDetailsId",
                table: "Department",
                newName: "OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_Department_OrganizationDetailsId",
                table: "Department",
                newName: "IX_Department_OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_OrganizationDetails_OrganizationId",
                table: "Department",
                column: "OrganizationId",
                principalTable: "OrganizationDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_OrganizationDetails_OrganizationId",
                table: "Department");

            migrationBuilder.RenameColumn(
                name: "OrganizationId",
                table: "Department",
                newName: "OrganizationDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_Department_OrganizationId",
                table: "Department",
                newName: "IX_Department_OrganizationDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_OrganizationDetails_OrganizationDetailsId",
                table: "Department",
                column: "OrganizationDetailsId",
                principalTable: "OrganizationDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
