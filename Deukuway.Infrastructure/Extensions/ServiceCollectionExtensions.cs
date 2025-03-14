using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Deukuway.Infrastructure.Extensions;

/// <summary>
/// Combined extension methods for configuring the service collection
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Configures all services for the application
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <param name="configuration">The configuration</param>
    public static void ConfigureAllServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure infrastructure
        services.ConfigureCors();
        //services.ConfigureIISIntegration();
        services.ConfigureLoggingService(configuration);
        
        // Configure repositories and database
        //services.ConfigureRepositoryManager();
        services.ConfigureSqlContext(configuration);
        
        // Configure services
        //services.ConfigureServiceManager();
        
        // Configure formatters
        //services.AddCustomMediaTypes();
        
        // Configure API features
        services.ConfigureVersioning();
        //services.ConfigureOutputCaching();
        services.ConfigureRateLimitingOptions();
        
        // Configure security
         services.AddAuthentication();
         services.ConfigureIdentity();
         services.ConfigureJwt(configuration);
         services.AddJwtConfiguration(configuration);
        
        // Configure documentation
        services.ConfigureSwagger();
    }
}