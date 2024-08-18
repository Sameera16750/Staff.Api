using Microsoft.Extensions.DependencyInjection;
using Staff.Application.Services.Implementations.Company;
using Staff.Application.Services.Interfaces.Company;

namespace Staff.Application.Configs;

public static class ApplicationDiConfigs
{
    public static IServiceCollection AddApplicationDependencyGroups(this IServiceCollection services)
    {
        services.AddScoped<ICompanyDetailService, CompanyDetailDetailService>();
        return services;
    }
}