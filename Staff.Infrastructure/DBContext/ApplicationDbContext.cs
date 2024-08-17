using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Staff.Infrastructure.DBContext;

public class ApplicationDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    
    public ApplicationDbContext( IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
    }
}