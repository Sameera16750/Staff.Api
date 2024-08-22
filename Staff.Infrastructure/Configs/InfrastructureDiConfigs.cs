using Microsoft.Extensions.DependencyInjection;
using Staff.Infrastructure.Repositories.Implementations.Organization;
using Staff.Infrastructure.Repositories.Interfaces.Organization;

namespace Staff.Infrastructure.Configs;

public static class InfrastructureDiConfigs
{
    public static IServiceCollection AddInfrastructureDependencyGroups(this IServiceCollection services)
    {
        services.AddScoped<IOrganizationDetailRepo, OrganizationDetailDetailRepo>();
        services.AddScoped<IDepartmentRepo, DepartmentRepo>();
        return services;
    }
}