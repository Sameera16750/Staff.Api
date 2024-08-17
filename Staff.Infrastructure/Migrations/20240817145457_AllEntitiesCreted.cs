using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Staff.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AllEntitiesCreted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Company",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Department_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeaveStatus",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StatusName = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeaveType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveType", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Salary",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaffMember",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Birthday = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    ContactNumber = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    DepartmentId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaffMember_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StaffMember_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StaffMember_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalaryUpdateLog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LastSalary = table.Column<double>(type: "double precision", nullable: false),
                    CurrentSalary = table.Column<double>(type: "double precision", nullable: false),
                    ChangedValue = table.Column<double>(type: "double precision", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    isIncrement = table.Column<bool>(type: "boolean", nullable: false),
                    SalaryId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryUpdateLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalaryUpdateLog_Salary_SalaryId",
                        column: x => x.SalaryId,
                        principalTable: "Salary",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StaffMemberId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CheckIn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CheckOut = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendance_StaffMember_StaffMemberId",
                        column: x => x.StaffMemberId,
                        principalTable: "StaffMember",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Leave",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StaffMemberId = table.Column<long>(type: "bigint", nullable: false),
                    LeaveTypeId = table.Column<long>(type: "bigint", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LeaveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    ApprovedById = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leave", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leave_LeaveStatus_LeaveStatusId",
                        column: x => x.LeaveStatusId,
                        principalTable: "LeaveStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Leave_LeaveType_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Leave_StaffMember_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "StaffMember",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Leave_StaffMember_StaffMemberId",
                        column: x => x.StaffMemberId,
                        principalTable: "StaffMember",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payroll",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StaffMemberId = table.Column<long>(type: "bigint", nullable: false),
                    SalaryId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payroll", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payroll_Salary_SalaryId",
                        column: x => x.SalaryId,
                        principalTable: "Salary",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payroll_StaffMember_StaffMemberId",
                        column: x => x.StaffMemberId,
                        principalTable: "StaffMember",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceReviews",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StaffMemberId = table.Column<long>(type: "bigint", nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReviewerId = table.Column<long>(type: "bigint", nullable: false),
                    ReviewRating = table.Column<double>(type: "double precision", nullable: false),
                    ReviewComment = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerformanceReviews_StaffMember_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "StaffMember",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerformanceReviews_StaffMember_StaffMemberId",
                        column: x => x.StaffMemberId,
                        principalTable: "StaffMember",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bonus",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    PayrollId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bonus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bonus_Payroll_PayrollId",
                        column: x => x.PayrollId,
                        principalTable: "Payroll",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Deductions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    PayrollId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deductions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deductions_Payroll_PayrollId",
                        column: x => x.PayrollId,
                        principalTable: "Payroll",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tax",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Rate = table.Column<double>(type: "double precision", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    PayrollId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tax", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tax_Payroll_PayrollId",
                        column: x => x.PayrollId,
                        principalTable: "Payroll",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_StaffMemberId",
                table: "Attendance",
                column: "StaffMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Bonus_PayrollId",
                table: "Bonus",
                column: "PayrollId");

            migrationBuilder.CreateIndex(
                name: "IX_Deductions_PayrollId",
                table: "Deductions",
                column: "PayrollId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_CompanyId",
                table: "Department",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Leave_ApprovedById",
                table: "Leave",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_Leave_LeaveStatusId",
                table: "Leave",
                column: "LeaveStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Leave_LeaveTypeId",
                table: "Leave",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Leave_StaffMemberId",
                table: "Leave",
                column: "StaffMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Payroll_SalaryId",
                table: "Payroll",
                column: "SalaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Payroll_StaffMemberId",
                table: "Payroll",
                column: "StaffMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceReviews_ReviewerId",
                table: "PerformanceReviews",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceReviews_StaffMemberId",
                table: "PerformanceReviews",
                column: "StaffMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryUpdateLog_SalaryId",
                table: "SalaryUpdateLog",
                column: "SalaryId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffMember_CompanyId",
                table: "StaffMember",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffMember_DepartmentId",
                table: "StaffMember",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffMember_RoleId",
                table: "StaffMember",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Tax_PayrollId",
                table: "Tax",
                column: "PayrollId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropTable(
                name: "Bonus");

            migrationBuilder.DropTable(
                name: "Deductions");

            migrationBuilder.DropTable(
                name: "Leave");

            migrationBuilder.DropTable(
                name: "PerformanceReviews");

            migrationBuilder.DropTable(
                name: "SalaryUpdateLog");

            migrationBuilder.DropTable(
                name: "Tax");

            migrationBuilder.DropTable(
                name: "LeaveStatus");

            migrationBuilder.DropTable(
                name: "LeaveType");

            migrationBuilder.DropTable(
                name: "Payroll");

            migrationBuilder.DropTable(
                name: "Salary");

            migrationBuilder.DropTable(
                name: "StaffMember");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Company");
        }
    }
}
