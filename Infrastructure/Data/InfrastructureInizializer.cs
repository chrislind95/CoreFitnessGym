using Infrastructure.Identity.Data;
using Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data;

public static class InfrastructureInitializer
{
    //public static async Task InitializeAsync(IServiceProvider serviceProvider, IConfiguration configuration, IWebHostEnvironment environment)
    //{
    //    ArgumentNullException.ThrowIfNull(configuration);
    //    ArgumentNullException.ThrowIfNull(environment);



    //    // Initialize database

    //    // Initialize default roles

    //    // Initialize default admin account
    //    await IdentityInitializer.AddDefaultAdminAsync(serviceProvider);
    //}

    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        // initialize database
        await PersistenceInitializer.InitializeDatabaseAsync(serviceProvider);

        // initialize default identity roles
        await IdentityInitializer.InitilizeDefaultRolesAsync(serviceProvider);

        // initialize default user accounts
        await IdentityInitializer.InitilizeDefaultAdminAccountsAsync(serviceProvider);
    }
}
