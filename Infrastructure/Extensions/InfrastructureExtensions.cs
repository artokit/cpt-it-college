using System.Reflection;
using FluentMigrator.Runner;
using Infrastructure.Dapper;
using Infrastructure.Dapper.Interfaces;
using Infrastructure.Dapper.Interfaces.Settings;
using Infrastructure.Interfaces.Repositories;
using Infrastructure.Repositories;
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

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }

    public static IServiceCollection AddMigrations(this IServiceCollection services, string connectionString)
    {
        services.AddFluentMigratorCore().ConfigureRunner(rb =>
                rb.AddPostgres()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider(false);
        return services;
    }
}
