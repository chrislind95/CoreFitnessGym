using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class AppUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? ImageUrl { get; set; }

    public static AppUser Create(string email, bool emailConfirmed = false, string? firstName = null, string? lastName = null, string? ImageUrl = null)
        => new()
        {
            UserName = email,
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            ImageUrl = ImageUrl,
            EmailConfirmed = emailConfirmed,
        };

    public static AppUser UpdateDetails(AppUser user, string? firstName, string? lastName,  string? imageUrl, string? phoneNumber)
    {
        if(user.FirstName != firstName)
            user.FirstName = firstName;

        if (user.LastName != lastName)
            user.LastName = lastName;

        if (user.ImageUrl != imageUrl)
            user.ImageUrl = imageUrl;

        if (user.PhoneNumber != phoneNumber)
            user.PhoneNumber = phoneNumber;

        return user;
    }
}
