using Microsoft.Extensions.DependencyInjection;
using Staff.Application.Helpers.ResourceHelper;
using Staff.Application.Helpers.ResponseHelper;
using Staff.Application.Services.Implementations.Organization;
using Staff.Application.Services.Interfaces.Organization;

namespace Staff.Application.Configs;

public static class ApplicationDiConfigs
{
    public static IServiceCollection AddApplicationDependencyGroups(this IServiceCollection services)
    {
        services.AddScoped<IResponseHelper, ResponseHelper>();
        services.AddScoped<IMessageResourceHelper, ResourceHelper.MessageResourceHelper>();
        services.AddScoped<IOrganizationDetailService, OrganizationDetailService>();
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IDesignationService, DesignationService>();
        services.AddScoped<IStaffMemberService, StaffMemberService>();
        return services;
    }
}