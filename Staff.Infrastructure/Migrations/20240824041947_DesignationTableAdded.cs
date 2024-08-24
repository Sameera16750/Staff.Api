using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Staff.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DesignationTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffMember_Department_DepartmentId",
                table: "StaffMember");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffMember_OrganizationDetails_OrganizationDetailsId",
                table: "StaffMember");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffMember_Roles_RoleId",
                table: "StaffMember");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_StaffMember_DepartmentId",
                table: "StaffMember");

            migrationBuilder.DropIndex(
                name: "IX_StaffMember_OrganizationDetailsId",
                table: "StaffMember");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "StaffMember");

            migrationBuilder.DropColumn(
                name: "OrganizationDetailsId",
                table: "StaffMember");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "StaffMember",
                newName: "DesignationId");

            migrationBuilder.RenameIndex(
                name: "IX_StaffMember_RoleId",
                table: "StaffMember",
                newName: "IX_StaffMember_DesignationId");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "StaffMember",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "Designation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DepartmentId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Designation_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Designation_DepartmentId",
                table: "Designation",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffMember_Designation_DesignationId",
                table: "StaffMember",
                column: "DesignationId",
                principalTable: "Designation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffMember_Designation_DesignationId",
                table: "StaffMember");

            migrationBuilder.DropTable(
                name: "Designation");

            migrationBuilder.RenameColumn(
                name: "DesignationId",
                table: "StaffMember",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_StaffMember_DesignationId",
                table: "StaffMember",
                newName: "IX_StaffMember_RoleId");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "StaffMember",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DepartmentId",
                table: "StaffMember",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationDetailsId",
                table: "StaffMember",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StaffMember_DepartmentId",
                table: "StaffMember",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffMember_OrganizationDetailsId",
                table: "StaffMember",
                column: "OrganizationDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffMember_Department_DepartmentId",
                table: "StaffMember",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffMember_OrganizationDetails_OrganizationDetailsId",
                table: "StaffMember",
                column: "OrganizationDetailsId",
                principalTable: "OrganizationDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffMember_Roles_RoleId",
                table: "StaffMember",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
