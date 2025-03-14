using Deukuway.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Deukuway.Infrastructure.Data;

public class RepositoryContext : IdentityDbContext<User>
{
    public RepositoryContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}