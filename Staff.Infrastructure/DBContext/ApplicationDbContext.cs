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
    
    public DbSet<EEmployee> Employee { get; set; }
}