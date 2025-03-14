using Microsoft.AspNetCore.Identity;

namespace Deukuway.Infrastructure.Persistence;

public class User : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
