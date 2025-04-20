using CRUDService.Validators;
using System.ComponentModel.DataAnnotations;

namespace CRUDService.DTOs
{
    // dTO class used for updates and creation requests,
    // it posses every field informative field of contact
    public class CreateContactRequest : DetailedContactDTO
    {
        [Required]
        public required int CategoryId { get; set; }

        [PasswordValidator(IsEdit = false)]
        public required string Password { get; set; }
    }
}
