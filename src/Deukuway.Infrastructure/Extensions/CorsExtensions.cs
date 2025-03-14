using Microsoft.Extensions.DependencyInjection;

namespace Deukuway.Infrastructure.Extensions;

/// <summary>
/// Extension methods for configuring CORS in the application
/// </summary>
public static class CorsExtensions
{
    /// <summary>
    /// Configures CORS policy
    /// </summary>
    /// <param name="services">The service collection</param>
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
}