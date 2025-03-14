using Deukuway.Infrastructure.Common;
using Deukuway.Infrastructure.Extensions;
using DotNetEnv;
using LoggerExtensions = Deukuway.Infrastructure.Extensions.LoggerExtensions;

// Load environment variables
EnvironmentLoader.LoadEnvironmentVariables();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Configure all services using the combined extensions
builder.Services.ConfigureAllServices(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "Deukuway API v1");
        s.SwaggerEndpoint("/swagger/v2/swagger.json", "Deukuway API v2");
    });
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
// Register log flushing when application is stopping
app.Lifetime.ApplicationStopping.Register(() => LoggerExtensions.CloseAndFlush());

await app.RunAsync();