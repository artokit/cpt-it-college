using Infrastructure.Interfaces.Settings;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Dapper;

public class DapperSettings : IDapperSettings
{
    public DapperSettings(IConfiguration configuration)
    {
        ConnectionString = configuration["ConnectionStrings:Database"]
            ?? throw new ArgumentException("ConnectionString in appsettings is missing");
    }

    public string ConnectionString { get; }
}