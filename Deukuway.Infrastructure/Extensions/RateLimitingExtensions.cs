using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Deukuway.Infrastructure.Extensions;


/// <summary>
/// Extension methods for configuring rate limiting
/// </summary>
public static class RateLimitingExtensions
{
    /// <summary>
    /// Configures rate limiting options
    /// </summary>
    /// <param name="services">The service collection</param>
    public static void ConfigureRateLimitingOptions(this IServiceCollection services)
    {
        services.AddRateLimiter(opt =>
        {
            opt.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
                RateLimitPartition.GetFixedWindowLimiter("GlobalLimiter",
                    partition => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = 30,
                        QueueLimit = 2,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        Window = TimeSpan.FromMinutes(1)
                    }));
           
            opt.AddPolicy("SpecificPolicy", context =>
                RateLimitPartition.GetFixedWindowLimiter("SpecificLimiter",
                    partition => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = 3,
                        Window = TimeSpan.FromSeconds(10)
                    }));

            opt.OnRejected = async (context, token) =>
            {
                context.HttpContext.Response.StatusCode = 429;

                if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                    await context.HttpContext.Response
                        .WriteAsync($"Too many requests. Please try again after {retryAfter.TotalSeconds} second(s).", token);
                else
                    await context.HttpContext.Response
                        .WriteAsync("Too many requests. Please try again later.", token);
            };
        });
    }
}