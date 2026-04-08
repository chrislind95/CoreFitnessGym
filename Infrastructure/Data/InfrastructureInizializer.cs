using Infrastructure.Persistence.Contexts;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data;

public static class InfrastructureInitializer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        // initialize database
        await PersistenceInitializer.InitializeDatabaseAsync(serviceProvider);

        // initialize default identity roles
        await IdentityInitializer.InitilizeDefaultRolesAsync(serviceProvider);

        // initialize default user accounts
        await IdentityInitializer.InitilizeDefaultAdminAccountsAsync(serviceProvider, configuration);
    }
}
