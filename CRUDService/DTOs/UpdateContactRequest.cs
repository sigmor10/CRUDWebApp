using CRUDService.Validators;
using System.ComponentModel.DataAnnotations;

namespace CRUDService.DTOs
{
    /// <summary>
    /// DTO class for use when recieving Contact update requests.
    /// Annotated for early valdiation and early rejection of invalid requests.
    /// </summary>
    public class UpdateContactRequest : DetailedContactDTO
    {
        [Required]
        public required int CategoryId { get; set; }

        [PasswordValidator(IsEdit = true)]
        public string? Password { get; set; }
    }
}
