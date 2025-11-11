using System.ComponentModel.DataAnnotations;

namespace DigiteonWeb.Common
{
    public class ContactRequest
    {
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public string SchoolName { get; set; } = string.Empty;

        [Required, EmailAddress] public string Email { get; set; } = string.Empty;

        [Required, Phone] public string PhoneNo { get; set; } = string.Empty;

        [Required] public string Message { get; set; } = string.Empty;
    }
}
