using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Staff.Core.Entities;

namespace Staff.Infrastructure.DBContext;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
    : DbContext(options)
{
    private readonly IConfiguration _configuration = configuration;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
    }

    public DbSet<Company> Company { get; set; }
    public DbSet<Department> Department { get; set; }
    public DbSet<StaffMember> StaffMember { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<Bonus> Bonus { get; set; }
    public DbSet<Deductions> Deductions { get; set; }
    public DbSet<Leave> Leave { get; set; }
    public DbSet<LeaveType> LeaveType { get; set; }
    public DbSet<LeaveStatus> LeaveStatus { get; set; }
    public DbSet<Payroll> Payroll  { get; set; }
    public DbSet<PerformanceReview> PerformanceReview  { get; set; }
    public DbSet<Salary> Salary  { get; set; }
    public DbSet<SalaryUpdateLog> SalaryUpdateLog  { get; set; }
    public DbSet<Tax> Tax  { get; set; }
}