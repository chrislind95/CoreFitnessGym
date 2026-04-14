namespace Application.Dtos.Identity;

public sealed record AuthResult(bool Succeeded, string? ErrorMessage = null)
{
    public static AuthResult Ok() => new(true);
    public static AuthResult Failed(string errorMessage) => new(false, errorMessage);
    public static AuthResult AlreadyExists() => new(true);
    public static AuthResult NotFound(string errorMessage = "User not found") => new(false, errorMessage);
}
