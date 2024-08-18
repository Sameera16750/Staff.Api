using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Staff.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CompanyTableUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_Company_CompanyId",
                table: "Department");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffMember_Company_CompanyId",
                table: "StaffMember");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "StaffMember",
                newName: "CompanyDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_StaffMember_CompanyId",
                table: "StaffMember",
                newName: "IX_StaffMember_CompanyDetailsId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Department",
                newName: "CompanyDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_Department_CompanyId",
                table: "Department",
                newName: "IX_Department_CompanyDetailsId");

            migrationBuilder.CreateTable(
                name: "CompanyDetail",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                newName: "CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_StaffMember_CompanyDetailsId",
                table: "StaffMember",
                newName: "IX_StaffMember_CompanyId");

            migrationBuilder.RenameColumn(
                name: "CompanyDetailsId",
                table: "Department",
                newName: "CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Department_CompanyDetailsId",
                table: "Department",
                newName: "IX_Department_CompanyId");

            migrationBuilder.CreateTable(
                name: "Company",
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
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Company_CompanyId",
                table: "Department",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffMember_Company_CompanyId",
                table: "StaffMember",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
