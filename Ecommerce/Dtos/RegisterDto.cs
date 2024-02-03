using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[a-z])|(?=.*[A-Z])|(?=.*\d)|(?=.*[^a-zA-Z\d])$",
            ErrorMessage = "Password should have atleast one lowercase | atleast one uppercase, should have atleast one number," +
            " should have atleast one special character")]
        public string Password { get; set; }
        [Compare("Password")]
        public string PasswordConfirmed { get; set; }

    }
}
