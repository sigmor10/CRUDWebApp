using CRUDService.Validators;
using System.ComponentModel.DataAnnotations;

namespace CRUDService.DTOs
{
    /// <summary>
    /// DTO class used for updates and creation requests,
    /// it posses every field informative field of contact.
    /// Annotated for early valdiation and early rejection of invalid requests.
    /// </summary>
    public class CreateContactRequest : DetailedContactDTO
    {
        [Required]
        public required int CategoryId { get; set; }

        [PasswordValidator(IsEdit = false)]
        public required string Password { get; set; }
    }
}
