using Infrastructure.Dapper;
using Infrastructure.Interfaces;
using Infrastructure.Interfaces.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddDapper(this IServiceCollection services)
    {
        services.AddScoped<IDapperSettings, DapperSettings>();
        services.AddScoped<IDapperContext, DapperContext>();
        return services;
    }
}