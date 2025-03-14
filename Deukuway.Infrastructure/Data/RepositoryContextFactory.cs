using Deukuway.Infrastructure.Common;

namespace Deukuway.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DotNetEnv;
using System.IO;

public class
    RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
{
    public RepositoryContext CreateDbContext(string[] args)
    {
        EnvironmentLoader.LoadEnvironmentVariables();
        
        string connectionString = EnvironmentLoader.GetConnectionString();

        var builder = new DbContextOptionsBuilder<RepositoryContext>()
            .UseNpgsql(connectionString,
                b => b.MigrationsAssembly("Deukuway.Infrastructure"));

        return new RepositoryContext(builder.Options);
    }
}