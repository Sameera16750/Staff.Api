using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Staff.Core.Entities.Attendance;
using Staff.Core.Entities.Organization;
using Staff.Core.Entities.Payroll;

namespace Staff.Infrastructure.DBContext;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
    : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
    }

    public DbSet<OrganizationDetails> Organization { get; set; }
    public DbSet<Department> Department { get; set; }
    public DbSet<Designation> Designation { get; set; }
    public DbSet<StaffMember> StaffMember { get; set; }
    public DbSet<AttendanceDetails> Attendances { get; set; }
    public DbSet<Bonus> Bonus { get; set; }
    public DbSet<Deductions> Deductions { get; set; }
    public DbSet<Leave> Leave { get; set; }
    public DbSet<LeaveType> LeaveType { get; set; }
    public DbSet<LeaveStatus> LeaveStatus { get; set; }
    public DbSet<Payroll> Payroll { get; set; }
    public DbSet<PerformanceReview> PerformanceReview { get; set; }
    public DbSet<Salary> Salary { get; set; }
    public DbSet<SalaryUpdateLog> SalaryUpdateLog { get; set; }
    public DbSet<Tax> Tax { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>()
            .HasOne<OrganizationDetails>(d => d.OrganizationDetails)
            .WithMany(o => o.Departments)
            .HasForeignKey(d => d.OrganizationId);

        modelBuilder.Entity<Designation>()
            .HasOne<Department>(d => d.Department)
            .WithMany(d => d.Designations)
            .HasForeignKey(d => d.DepartmentId);

        modelBuilder.Entity<StaffMember>()
            .HasOne<Designation>(s => s.Designation)
            .WithMany(d => d.StaffMembers)
            .HasForeignKey(s => s.DesignationId);

        modelBuilder.Entity<PerformanceReview>()
            .HasOne<StaffMember>(p => p.Reviewer)
            .WithMany(s => s.ReviewGiven)
            .HasForeignKey(p => p.ReviewerId);

        modelBuilder.Entity<PerformanceReview>()
            .HasOne<StaffMember>(p => p.StaffMember)
            .WithMany(s => s.ReviewReceived)
            .HasForeignKey(p => p.StaffMemberId);

        modelBuilder.Entity<AttendanceDetails>()
            .HasOne<StaffMember>(a => a.StaffMember)
            .WithMany(s => s.AttendanceDetails)
            .HasForeignKey(a => a.StaffMemberId);

        modelBuilder.Entity<LeaveType>()
            .HasOne<OrganizationDetails>(l => l.OrganizationDetails)
            .WithMany(o => o.LeaveTypes)
            .HasForeignKey(l => l.OrganizationId);
    }
}