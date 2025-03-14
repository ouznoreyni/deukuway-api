using Deukuway.Infrastructure.Common;

namespace Deukuway.Infrastructure.Extensions;
using Deukuway.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extension methods for configuring SQL database context
/// </summary>
public static class SqlExtensions
{
    /// <summary>
    /// Configures SQL context with PostgreSQL
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <param name="configuration">The configuration</param>
    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
    {
        EnvironmentLoader.LoadEnvironmentVariables();
        
        string connectionString = EnvironmentLoader.GetConnectionString();

        // Register the DbContext with the connection string
        services.AddDbContext<RepositoryContext>(options =>
            options.UseNpgsql(connectionString, 
                b => b.MigrationsAssembly("Deukuway.Infrastructure")));
    }
    
    /// <summary>
    /// Configures repository manager services
    /// </summary>
    /// <param name="services">The service collection</param>
    public static void ConfigureRepositoryManager(this IServiceCollection services)
    {
        // Register your repository manager and repositories here
        // Example:
        // services.AddScoped<IRepositoryManager, RepositoryManager>();
        // services.AddScoped<IUserRepository, UserRepository>();
    }
}