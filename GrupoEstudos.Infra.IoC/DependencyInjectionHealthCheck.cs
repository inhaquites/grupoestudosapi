using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace GrupoEstudos.Infra.IoC;

public static class DependencyInjectionHealthCheck
{
    public static IServiceCollection AddInfrastructureHealthCheck(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddSqlServer(
                connectionString: configuration.GetConnectionString("DefaultConnectionAuth"),
                healthQuery: "SELECT 1;",
                name: "sqlserver",
                failureStatus: HealthStatus.Degraded,
                tags: new string[] { "db", "data", "sqlserver" })
            .AddMySql(
                connectionString: configuration.GetConnectionString("DefaultConnectionAuth"),
                name: "mysql",
                failureStatus: HealthStatus.Degraded,
                tags: new string[] { "db", "data", "mysql" })            
            .AddRedis(
                "localhost:6379",
                name: "redis",
                tags: new string[] { "db", "cache", "redis" });

        return services;
    }
}
