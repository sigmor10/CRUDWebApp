using FrontEnd.Validators;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    /// <summary>
    /// dTO class used for updates and creation requests,
    /// it posses every field informative field of contact
    /// Annotated for early valdiation and early rejection of invalid requests.
    /// </summary>
    public class CreateContactRequest : DetailedContact
    {
        [Required]
        public int? CategoryId { get; set; }

        [PasswordValidator(IsEdit = false)]
        public string? Password { get; set; }
    }
}
