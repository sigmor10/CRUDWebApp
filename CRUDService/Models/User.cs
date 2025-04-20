using CRUDService.Validators;
using System.ComponentModel.DataAnnotations;

namespace CRUDService.Models
{
    public class User
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [PasswordValidator(IsEdit = false)]
        public required string Password { get; set; }
    }
}
