using Microsoft.Extensions.DependencyInjection;
using Staff.Infrastructure.Repositories.Implementations.company;
using Staff.Infrastructure.Repositories.Interfaces.company;

namespace Staff.Infrastructure.Configs;

public static class InfrastructureDiConfigs
{
    public static IServiceCollection AddInfrastructureDependencyGroups(this IServiceCollection services)
    {
        services.AddScoped<ICompanyDetailRepo, CompanyDetailDetailRepo>();
        return services;
    }
}