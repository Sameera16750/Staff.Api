﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Staff.Infrastructure.DBContext;

#nullable disable

namespace Staff.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240817145740_RequiredUpdates")]
    partial class RequiredUpdates
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Staff.Core.Entities.Attendance", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CheckIn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CheckOut")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("StaffMemberId")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("StaffMemberId");

                    b.ToTable("Attendance");
                });

            modelBuilder.Entity("Staff.Core.Entities.Bonus", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("PayrollId")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PayrollId");

                    b.ToTable("Bonus");
                });

            modelBuilder.Entity("Staff.Core.Entities.Company", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ContactNo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("Staff.Core.Entities.Deductions", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("PayrollId")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PayrollId");

                    b.ToTable("Deductions");
                });

            modelBuilder.Entity("Staff.Core.Entities.Department", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("CompanyId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("Staff.Core.Entities.Leave", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ApprovedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("LeaveStatusId")
                        .HasColumnType("bigint");

                    b.Property<long>("LeaveTypeId")
                        .HasColumnType("bigint");

                    b.Property<long>("StaffMemberId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ApprovedById");

                    b.HasIndex("LeaveStatusId");

                    b.HasIndex("LeaveTypeId");

                    b.HasIndex("StaffMemberId");

                    b.ToTable("Leave");
                });

            modelBuilder.Entity("Staff.Core.Entities.LeaveStatus", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("LeaveStatus");
                });

            modelBuilder.Entity("Staff.Core.Entities.LeaveType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("LeaveType");
                });

            modelBuilder.Entity("Staff.Core.Entities.Payroll", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("SalaryId")
                        .HasColumnType("bigint");

                    b.Property<long>("StaffMemberId")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SalaryId");

                    b.HasIndex("StaffMemberId");

                    b.ToTable("Payroll");
                });

            modelBuilder.Entity("Staff.Core.Entities.PerformanceReview", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("ReviewComment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ReviewDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("ReviewRating")
                        .HasColumnType("double precision");

                    b.Property<long>("ReviewerId")
                        .HasColumnType("bigint");

                    b.Property<long>("StaffMemberId")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ReviewerId");

                    b.HasIndex("StaffMemberId");

                    b.ToTable("PerformanceReviews");
                });

            modelBuilder.Entity("Staff.Core.Entities.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Staff.Core.Entities.Salary", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Salary");
                });

            modelBuilder.Entity("Staff.Core.Entities.SalaryUpdateLog", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<double>("ChangedValue")
                        .HasColumnType("double precision");

                    b.Property<double>("CurrentSalary")
                        .HasColumnType("double precision");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("LastSalary")
                        .HasColumnType("double precision");

                    b.Property<long?>("SalaryId")
                        .HasColumnType("bigint");

                    b.Property<bool>("isIncrement")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("SalaryId");

                    b.ToTable("SalaryUpdateLog");
                });

            modelBuilder.Entity("Staff.Core.Entities.StaffMember", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("CompanyId")
                        .HasColumnType("bigint");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("DepartmentId")
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("RoleId");

                    b.ToTable("StaffMember");
                });

            modelBuilder.Entity("Staff.Core.Entities.Tax", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("PayrollId")
                        .HasColumnType("bigint");

                    b.Property<double>("Rate")
                        .HasColumnType("double precision");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PayrollId");

                    b.ToTable("Tax");
                });

            modelBuilder.Entity("Staff.Core.Entities.Attendance", b =>
                {
                    b.HasOne("Staff.Core.Entities.StaffMember", "StaffMember")
                        .WithMany()
                        .HasForeignKey("StaffMemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StaffMember");
                });

            modelBuilder.Entity("Staff.Core.Entities.Bonus", b =>
                {
                    b.HasOne("Staff.Core.Entities.Payroll", null)
                        .WithMany("Bonuses")
                        .HasForeignKey("PayrollId");
                });

            modelBuilder.Entity("Staff.Core.Entities.Deductions", b =>
                {
                    b.HasOne("Staff.Core.Entities.Payroll", null)
                        .WithMany("Deductions")
                        .HasForeignKey("PayrollId");
                });

            modelBuilder.Entity("Staff.Core.Entities.Department", b =>
                {
                    b.HasOne("Staff.Core.Entities.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Staff.Core.Entities.Leave", b =>
                {
                    b.HasOne("Staff.Core.Entities.StaffMember", "ApprovedBy")
                        .WithMany()
                        .HasForeignKey("ApprovedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Staff.Core.Entities.LeaveStatus", "LeaveStatus")
                        .WithMany()
                        .HasForeignKey("LeaveStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Staff.Core.Entities.LeaveType", "LeaveType")
                        .WithMany()
                        .HasForeignKey("LeaveTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Staff.Core.Entities.StaffMember", "StaffMember")
                        .WithMany()
                        .HasForeignKey("StaffMemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApprovedBy");

                    b.Navigation("LeaveStatus");

                    b.Navigation("LeaveType");

                    b.Navigation("StaffMember");
                });

            modelBuilder.Entity("Staff.Core.Entities.Payroll", b =>
                {
                    b.HasOne("Staff.Core.Entities.Salary", "Salary")
                        .WithMany()
                        .HasForeignKey("SalaryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Staff.Core.Entities.StaffMember", "StaffMember")
                        .WithMany()
                        .HasForeignKey("StaffMemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Salary");

                    b.Navigation("StaffMember");
                });

            modelBuilder.Entity("Staff.Core.Entities.PerformanceReview", b =>
                {
                    b.HasOne("Staff.Core.Entities.StaffMember", "Reviewer")
                        .WithMany()
                        .HasForeignKey("ReviewerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Staff.Core.Entities.StaffMember", "StaffMember")
                        .WithMany()
                        .HasForeignKey("StaffMemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reviewer");

                    b.Navigation("StaffMember");
                });

            modelBuilder.Entity("Staff.Core.Entities.SalaryUpdateLog", b =>
                {
                    b.HasOne("Staff.Core.Entities.Salary", null)
                        .WithMany("SalaryUpdateLogs")
                        .HasForeignKey("SalaryId");
                });

            modelBuilder.Entity("Staff.Core.Entities.StaffMember", b =>
                {
                    b.HasOne("Staff.Core.Entities.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Staff.Core.Entities.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Staff.Core.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Department");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Staff.Core.Entities.Tax", b =>
                {
                    b.HasOne("Staff.Core.Entities.Payroll", null)
                        .WithMany("Taxes")
                        .HasForeignKey("PayrollId");
                });

            modelBuilder.Entity("Staff.Core.Entities.Payroll", b =>
                {
                    b.Navigation("Bonuses");

                    b.Navigation("Deductions");

                    b.Navigation("Taxes");
                });

            modelBuilder.Entity("Staff.Core.Entities.Salary", b =>
                {
                    b.Navigation("SalaryUpdateLogs");
                });
#pragma warning restore 612, 618
        }
    }
}
