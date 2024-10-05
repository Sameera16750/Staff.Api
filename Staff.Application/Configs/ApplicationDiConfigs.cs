using Microsoft.Extensions.DependencyInjection;
using Staff.Application.Helpers.DateFormatHelper;
using Staff.Application.Helpers.ResourceHelper;
using Staff.Application.Helpers.ResponseHelper;
using Staff.Application.Helpers.SecurityHelper;
using Staff.Application.Services.Implementations.Attendance;
using Staff.Application.Services.Implementations.Organization;
using Staff.Application.Services.Interfaces.Attendance;
using Staff.Application.Services.Interfaces.Organization;

namespace Staff.Application.Configs;

public static class ApplicationDiConfigs
{
    public static IServiceCollection AddApplicationDependencyGroups(this IServiceCollection services)
    {
        services.AddScoped<IResponseHelper, ResponseHelper>();
        services.AddScoped<ISecurityHelper, SecurityHelper>();
        services.AddScoped<IDateFormatHelper, DateFormatHelper>();
        services.AddScoped<IMessageResourceHelper, ResourceHelper.MessageResourceHelper>();
        services.AddScoped<IOrganizationDetailService, OrganizationDetailService>();
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IDesignationService, DesignationService>();
        services.AddScoped<IStaffMemberService, StaffMemberService>();
        services.AddScoped<IPerformanceReviewService, PerformanceReviewService>();
        services.AddScoped<IAttendanceDetailsService, AttendanceDetailsService>();
        services.AddScoped<ILeaveService, LeaveService>();
        return services;
    }
}