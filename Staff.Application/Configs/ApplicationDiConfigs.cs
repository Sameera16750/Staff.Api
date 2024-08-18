using Microsoft.Extensions.DependencyInjection;
using Staff.Application.Services.Implementations.Organization;
using Staff.Application.Services.Interfaces.Organization;

namespace Staff.Application.Configs;

public static class ApplicationDiConfigs
{
    public static IServiceCollection AddApplicationDependencyGroups(this IServiceCollection services)
    {
        services.AddScoped<IOrganizationDetailService, OrganizationDetailService>();
        return services;
    }
}