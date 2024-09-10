using Microsoft.Extensions.DependencyInjection;
using Staff.Infrastructure.Repositories.Implementations.Attendance;
using Staff.Infrastructure.Repositories.Implementations.Organization;
using Staff.Infrastructure.Repositories.Interfaces.Attendance;
using Staff.Infrastructure.Repositories.Interfaces.Organization;

namespace Staff.Infrastructure.Configs;

public static class InfrastructureDiConfigs
{
    public static IServiceCollection AddInfrastructureDependencyGroups(this IServiceCollection services)
    {
        services.AddScoped<IOrganizationDetailRepo, OrganizationDetailDetailRepo>();
        services.AddScoped<IDepartmentRepo, DepartmentRepo>();
        services.AddScoped<IDesignationRepo, DesignationRepo>();
        services.AddScoped<IStaffMemberRepo, StaffMemberReo>();
        services.AddScoped<IPerformanceReviewRepo, PerformanceReviewRepo>();
        services.AddScoped<IAttendanceDetailsRepo, AttendanceDetailsRepo>();
        return services;
    }
}