using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Deukuway.Infrastructure.Extensions;

/// <summary>
/// Extension methods for configuring Swagger
/// </summary>
public static class SwaggerExtensions
{
    /// <summary>
    /// Configures Swagger documentation
    /// </summary>
    /// <param name="services">The service collection</param>
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc("v1", new OpenApiInfo() 
            { 
                Title = "Deukuway API", 
                Version = "v1",
                Description = "Real estate system API by Ouznoreyni",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Ousmane DIOP",
                    Email = "ousmanediopp268@gmail.com",
                    Url = new Uri("https://x.com/ouznoreyni221"),
                },
                License = new OpenApiLicense
                {
                    Name = "Deukuway API LICX",
                    Url = new Uri("https://example.com/license"),
                }
            });
            s.SwaggerDoc("v2", new OpenApiInfo { Title = "Code Maze API", Version = "v2" });

            //var xmlFile = $"{typeof(Presentation.AssemblyReference).Assembly.GetName().Name}.xml";
            //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //s.IncludeXmlComments(xmlPath);

            s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Place to add JWT with Bearer",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            s.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Name = "Bearer",
                    },
                    new List<string>()
                }
            });
        });
    } 
}