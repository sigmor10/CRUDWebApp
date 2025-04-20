using FrontEnd.Validators;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    /// <summary>
    /// Class representing a user.
    /// Annotated for early valdiation and early rejection of invalid requests.
    /// </summary>
    public class User
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [PasswordValidator(IsEdit = false)]
        public string? Password { get; set; }
    }
}
