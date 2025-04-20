using CRUDService.Validators;
using System.ComponentModel.DataAnnotations;

namespace CRUDService.Models
{
    /// <summary>
    /// Class representing a user
    /// </summary>
    public class User
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [PasswordValidator(IsEdit = false)]
        public required string Password { get; set; }
    }
}
