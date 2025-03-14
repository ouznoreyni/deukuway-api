using Asp.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace Deukuway.Infrastructure.Extensions;

/// <summary>
/// Extension methods for configuring API versioning
/// </summary>
public static class VersioningExtensions
{
    /// <summary>
    /// Configures API versioning
    /// </summary>
    /// <param name="services">The service collection</param>
    public static void ConfigureVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ApiVersionReader = new HeaderApiVersionReader("api-version");
            })
            .AddMvc(opt =>
            {
              //  opt.Conventions.Controller<CompaniesController>()
              //      .HasApiVersion(new ApiVersion(1, 0));
              //  opt.Conventions.Controller<CompaniesV2Controller>()
              //      .HasDeprecatedApiVersion(new ApiVersion(2, 0));
            });
    }
}