using System.ComponentModel.DataAnnotations;

namespace Presentation.WebbApp.Models.CustomerService;

public class ContactForm
{
    [Required(ErrorMessage = "First name is required")]
    [DataType(DataType.Text)]
    [Display(Name = "First Name", Prompt = "Enter first name")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Last name is required")]
    [DataType(DataType.Text)]
    [Display(Name = "Last Name", Prompt = "Enter last name")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "Email is required")]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email", Prompt = "username@example.com")]
    public string Email { get; set; } = null!;

    [Phone(ErrorMessage = "Phone number must be valid")]
    [DataType(DataType.PhoneNumber)]
    [Display(Name = "Phone Number", Prompt = "ex. 070-123 45 67")]
    public string? Phone { get; set; }

    [Required(ErrorMessage = "Message is required")]
    [DataType(DataType.MultilineText)]
    [Display(Name = "Message", Prompt = "Enter message")]
    public string Message { get; set; } = null!;

    [Range(typeof(bool), "true", "true", ErrorMessage = "You must accept to save personal information")]
    [Display(Name = "I accept that CoreFitness saves my information")]
    public bool AcceptSavePersonalInformation { get; set; }
}
