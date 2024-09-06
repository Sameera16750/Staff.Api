using Microsoft.OpenApi.Models;
using Staff.Api.Middlewares;
using Staff.Application.Configs;
using Staff.Core.Constants;
using Staff.Infrastructure.Configs;
using Staff.Infrastructure.DBContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Add support for custom headers like API Key
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = Constants.Headers.ApiKeyHeader, // Custom header name
        Type = SecuritySchemeType.ApiKey,
        Description = "API Key authentication"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            []
        }
    });
});

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>();

builder.Services.AddApplicationDependencyGroups();
builder.Services.AddInfrastructureDependencyGroups();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ApiKeyMiddleware>();
app.MapControllers();
app.UseHttpsRedirection();
app.Run();