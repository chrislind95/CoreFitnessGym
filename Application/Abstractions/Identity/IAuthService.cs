using Application.Dtos.Identity;

namespace Application.Abstractions.Identity;

public interface IAuthService
{
    Task<AuthResult> CreateUserAsync(string email, string password, string? roleName = null);
    Task<AuthResult> LoginUserAsync(string email, string password, bool rememberMe = false);
    Task<AuthResult> UserExistsAsync(string email);

    Task LogoutUserAsync();
}
