namespace Application.Dtos.Identity;

public enum AuthErrorType
{
    InvalidCredentials,
    RequireTwoFactorAuth,
    LockedOut,
    NotAllowed,
    Error
}
