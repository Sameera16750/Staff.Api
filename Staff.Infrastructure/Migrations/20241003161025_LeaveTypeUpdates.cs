using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Staff.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LeaveTypeUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OrganizationId",
                table: "LeaveType",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_LeaveType_OrganizationId",
                table: "LeaveType",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveType_OrganizationDetails_OrganizationId",
                table: "LeaveType",
                column: "OrganizationId",
                principalTable: "OrganizationDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveType_OrganizationDetails_OrganizationId",
                table: "LeaveType");

            migrationBuilder.DropIndex(
                name: "IX_LeaveType_OrganizationId",
                table: "LeaveType");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "LeaveType");
        }
    }
}
