using Deukuway.Infrastructure.Data;
using Deukuway.Infrastructure.Persistence;

namespace Deukuway.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;


/// <summary>
/// Extension methods for configuring identity
/// </summary>
public static class IdentityExtensions
{
    /// <summary>
    /// Configures identity services
    /// </summary>
    /// <param name="services">The service collection</param>
    public static void ConfigureIdentity(this IServiceCollection services)
    {
        var builder = services.AddIdentity<User, IdentityRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 10;
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<RepositoryContext>()
            .AddDefaultTokenProviders();
    }
}