using CRUDService.Validators;
using System.ComponentModel.DataAnnotations;

namespace CRUDService.DTOs
{
    // DTO class for use when recieving Contact update requests
    public class UpdateContactRequest : DetailedContactDTO
    {
        [Required]
        public required int CategoryId { get; set; }

        [PasswordValidator(IsEdit = true)]
        public string? Password { get; set; }
    }
}
