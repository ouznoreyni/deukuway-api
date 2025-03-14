namespace Deukuway.Infrastructure.Configuration;
using System;

/// <summary>
/// Configuration class for JWT settings
/// </summary>
public class JwtConfiguration
{
    /// <summary>
    /// Section name in appsettings.json
    /// </summary>
    public string Section { get; set; } = "JwtSettings";

    /// <summary>
    /// Valid issuer for the JWT token
    /// </summary>
    public string ValidIssuer { get; set; }

    /// <summary>
    /// Valid audience for the JWT token
    /// </summary>
    public string ValidAudience { get; set; }

    /// <summary>
    /// Token expiration in minutes
    /// </summary>
    public string Expires { get; set; }
}