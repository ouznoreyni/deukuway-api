using DotNetEnv;

namespace Deukuway.Infrastructure.Common;

public static class EnvironmentLoader
{
    private static bool _loaded = false;
    private static readonly object _lock = new object();

    public static void LoadEnvironmentVariables()
    {
        // Only load once - thread-safe implementation
        if (!_loaded)
        {
            lock (_lock)
            {
                if (!_loaded)
                {
                    try
                    {
                        // First try to load from the current directory
                        string currentDir = Directory.GetCurrentDirectory();
                        string envPath = Path.Combine(currentDir, ".env");

                        if (File.Exists(envPath))
                        {
                            Env.Load(envPath);
                            Console.WriteLine(
                                $"Loaded environment variables from {envPath}");
                        }
                        else
                        {
                            // Try parent directory
                            DirectoryInfo? parentDir =
                                Directory.GetParent(currentDir);
                            if (parentDir != null)
                            {
                                envPath = Path.Combine(parentDir.FullName,
                                    ".env");
                                if (File.Exists(envPath))
                                {
                                    Env.Load(envPath);
                                    Console.WriteLine(
                                        $"Loaded environment variables from {envPath}");
                                }
                                else
                                {
                                    // Try parent of parent directory (solution root)
                                    DirectoryInfo? solutionDir =
                                        parentDir.Parent;
                                    if (solutionDir != null)
                                    {
                                        envPath =
                                            Path.Combine(solutionDir.FullName,
                                                ".env");
                                        if (File.Exists(envPath))
                                        {
                                            Env.Load(envPath);
                                            Console.WriteLine(
                                                $"Loaded environment variables from {envPath}");
                                        }
                                        else
                                        {
                                            Console.WriteLine(
                                                "Could not find .env file in current, parent, or solution directories");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine(
                                    "Current directory does not have a parent directory");
                            }
                        }

                        // Optional: Debug output
                        if (!string.IsNullOrEmpty(
                                Environment.GetEnvironmentVariable(
                                    "DB_PASSWORD")))
                        {
                            Console.WriteLine(
                                "DB password loaded successfully");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(
                            $"Error loading environment variables: {ex.Message}");
                    }

                    _loaded = true;
                }
            }
        }
    }

    // Helper method to get connection string
    public static string GetConnectionString()
    {
        LoadEnvironmentVariables();

        // Use "localhost" instead of "postgres" for local development
        string host = Environment.GetEnvironmentVariable("DB_HOST") ??
                      "localhost";
        if (host == "postgres" &&
            Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ==
            "Development")
        {
            host = "localhost";
        }

        string port = Environment.GetEnvironmentVariable("DB_PORT") ?? "5432";
        string database = Environment.GetEnvironmentVariable("DB_NAME") ??
                          "deukuway";
        string username = Environment.GetEnvironmentVariable("DB_USER") ??
                          "postgres";
        string password = Environment.GetEnvironmentVariable("DB_PASSWORD") ??
                          "postgres_password";

        return
            $"Host={host};Port={port};Database={database};Username={username};Password={password}";
    }
}