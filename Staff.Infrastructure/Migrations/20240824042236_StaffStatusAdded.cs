using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Staff.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StaffStatusAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "StaffMember",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "StaffMember");
        }
    }
}
