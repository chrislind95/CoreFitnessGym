using Application.Abstractions.Identity;
using Application.Dtos.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.Services;

public class IdentityAuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager) : IAuthService
{
    public async Task<AuthResult> UserExistsAsync(string email)
    {
        if(string.IsNullOrWhiteSpace(email))
            throw new ArgumentNullException(nameof(email));

        var exists = await userManager.Users.AnyAsync(x =>  x.Email == email);
        return exists ? AuthResult.AlreadyExists() : AuthResult.NotFound();
    }

    public async Task<AuthResult> CreateUserAsync(string email, string password, string? roleName = null)
    {
        if(string.IsNullOrEmpty(email))
            throw new ArgumentNullException(nameof(email));

        if (string.IsNullOrEmpty(password))
            throw new ArgumentNullException(nameof(password));

        var exists = await UserExistsAsync(email);
        if (exists.Succeeded)
            return AuthResult.Failed("User with same email already exists");

        var user = AppUser.Create(email, true);

        var created = await userManager.CreateAsync(user, password);
        if (created.Succeeded)
        {
            if (!string.IsNullOrWhiteSpace(roleName))
            {
                if(await roleManager.RoleExistsAsync(roleName))
                await userManager.AddToRoleAsync(user, roleName);
            }
        }
        
        return created.Succeeded ? AuthResult.Ok() : AuthResult.Failed(created.Errors.FirstOrDefault()?.Description ?? "Unable to crate user");
    }

    public async Task<AuthResult> LoginUserAsync(string email, string password, bool rememberMe = false)
    {
        if (string.IsNullOrEmpty(email))
            throw new ArgumentNullException(nameof(email));

        if (string.IsNullOrEmpty(password))
            throw new ArgumentNullException(nameof(password));

        var exists = await UserExistsAsync(email);
        if (!exists.Succeeded)
            return AuthResult.Failed("Incorrect email or password");

        var result = await signInManager.PasswordSignInAsync(email, password, rememberMe, false);

        if (result.IsLockedOut)
            return AuthResult.Failed("User has been temporary locked out");

        if (result.IsNotAllowed)
            return AuthResult.Failed("User is not allowed to sign in");

        if (!result.Succeeded)
            return AuthResult.Failed("Incorrect email or password");

        return AuthResult.Ok();
    }

    public Task LogoutUserAsync() => signInManager.SignOutAsync();
}
