using FrontEnd.Validators;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class User
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [PasswordValidator(IsEdit = false)]
        public string? Password { get; set; }
    }
}
