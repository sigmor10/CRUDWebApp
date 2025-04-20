using FrontEnd.Validators;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    // dTO class used for updates and creation requests,
    // it posses every field informative field of contact
    public class CreateContactRequest : DetailedContact
    {
        [Required]
        public int? CategoryId { get; set; }

        [PasswordValidator(IsEdit = false)]
        public string? Password { get; set; }
    }
}
