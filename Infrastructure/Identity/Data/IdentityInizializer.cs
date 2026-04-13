using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Identity.Data;

internal class IdentityInitializer()
{

    public static async Task InitilizeDefaultRolesAsync(IServiceProvider serviceProvider)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        var roles = new List<IdentityRole>()
        {
            new("Admin"),
            new("Member")
        };

        foreach (var role in roles)
            if (!await roleManager.RoleExistsAsync(role.Name!))
                await roleManager.CreateAsync(role);
    }

    public static async Task InitilizeDefaultAdminAccountsAsync(IServiceProvider serviceProvider)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        var admins = new List<AppUser>
        {
            AppUser.Create("admin@domain.local", true)
        };

        if (!await userManager.Users.AnyAsync())
        {
            var password = "BytMig123!";
            var roleName = "Admin";

            foreach (var user in admins)
            {
                var created = await userManager.CreateAsync(user, password);
                if (created.Succeeded)
                {
                    if (await roleManager.RoleExistsAsync(roleName))
                        await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }
    }
}