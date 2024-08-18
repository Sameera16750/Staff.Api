using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Staff.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrganizationTableUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_CompanyDetail_CompanyDetailsId",
                table: "Department");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffMember_CompanyDetail_CompanyDetailsId",
                table: "StaffMember");

            migrationBuilder.DropTable(
                name: "CompanyDetail");

            migrationBuilder.RenameColumn(
                name: "CompanyDetailsId",
                table: "StaffMember",
                newName: "OrganizationDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_StaffMember_CompanyDetailsId",
                table: "StaffMember",
                newName: "IX_StaffMember_OrganizationDetailsId");

            migrationBuilder.RenameColumn(
                name: "CompanyDetailsId",
                table: "Department",
                newName: "OrganizationDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_Department_CompanyDetailsId",
                table: "Department",
                newName: "IX_Department_OrganizationDetailsId");

            migrationBuilder.CreateTable(
                name: "OrganizationDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    ContactNo = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationDetails", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Department_OrganizationDetails_OrganizationDetailsId",
                table: "Department",
                column: "OrganizationDetailsId",
                principalTable: "OrganizationDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffMember_OrganizationDetails_OrganizationDetailsId",
                table: "StaffMember",
                column: "OrganizationDetailsId",
                principalTable: "OrganizationDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_OrganizationDetails_OrganizationDetailsId",
                table: "Department");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffMember_OrganizationDetails_OrganizationDetailsId",
                table: "StaffMember");

            migrationBuilder.DropTable(
                name: "OrganizationDetails");

            migrationBuilder.RenameColumn(
                name: "OrganizationDetailsId",
                table: "StaffMember",
                newName: "CompanyDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_StaffMember_OrganizationDetailsId",
                table: "StaffMember",
                newName: "IX_StaffMember_CompanyDetailsId");

            migrationBuilder.RenameColumn(
                name: "OrganizationDetailsId",
                table: "Department",
                newName: "CompanyDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_Department_OrganizationDetailsId",
                table: "Department",
                newName: "IX_Department_CompanyDetailsId");

            migrationBuilder.CreateTable(
                name: "CompanyDetail",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Address = table.Column<string>(type: "text", nullable: false),
                    ContactNo = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDetail", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Department_CompanyDetail_CompanyDetailsId",
                table: "Department",
                column: "CompanyDetailsId",
                principalTable: "CompanyDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffMember_CompanyDetail_CompanyDetailsId",
                table: "StaffMember",
                column: "CompanyDetailsId",
                principalTable: "CompanyDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
