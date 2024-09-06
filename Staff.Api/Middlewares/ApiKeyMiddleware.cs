using Microsoft.EntityFrameworkCore;
using Staff.Application.Models.Response.Common;
using Staff.Core.Constants;
using Staff.Infrastructure.DBContext;

namespace Staff.Api.Middlewares;

public class ApiKeyMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, ApplicationDbContext dbContext)
    {
        if (!context.Request.Headers.TryGetValue(Constants.Headers.ApiKeyHeader, out var extractedApiKey))
        {
            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsJsonAsync(new MessageResponse { Message = "API Key is missing." });
            return;
        }

        var organization = await dbContext.Organization.FirstOrDefaultAsync(o =>
            o.ApiKey.Equals(extractedApiKey) && o.Status == Constants.Status.Active);

        if (organization == null)
        {
            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsJsonAsync(new MessageResponse { Message = "Unauthorized client." });
            return;
        }

        context.Items[Constants.Headers.OrganizationId] = organization.Id;
        await next(context); // Call the next
    }
}