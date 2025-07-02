using Microsoft.EntityFrameworkCore;
using LegalSaaS.Api.Dal.Database;

namespace LegalSaaS.Api.Dal.Extensions;

public static class DatabaseExtensions
{
    /// <summary>
    /// Ensures the database is created and migrated
    /// </summary>
    public static async Task<IHost> MigrateDatabaseAsync(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContext>>();

        try
        {
            logger.LogInformation("Starting database migration...");
            
            // Check if database exists
            var canConnect = await context.Database.CanConnectAsync();
            if (!canConnect)
            {
                logger.LogInformation("Database does not exist. Creating database...");

                await context.Database.EnsureCreatedAsync();
            }

            // Apply any pending migrations
            var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
            if (!pendingMigrations.Any())
            {
                logger.LogInformation("Database is up to date. No migrations needed.");

                return host;
            }

            logger.LogInformation(
                "Applying {Count} pending migrations: {Migrations}",
                pendingMigrations.Count(),
                string.Join(", ", pendingMigrations));

            await context.Database.MigrateAsync();

            logger.LogInformation("Database migration completed successfully");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while migrating the database");

            throw;
        }

        return host;
    }

    /// <summary>
    /// Seeds initial data if needed
    /// </summary>
    public static async Task<IHost> SeedDatabaseAsync(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContext>>();

        try
        {
            // Add seed data here if needed
            if (!await context.Customers.AnyAsync())
            {
                logger.LogInformation("No customers found. Database is ready for use.");

                // Seed data here if needed, for example:
                // await context.Customers.AddRangeAsync(seedCustomers);
                // await context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database");

            throw;
        }

        return host;
    }
}
