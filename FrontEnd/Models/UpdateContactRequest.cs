using FrontEnd.Validators;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    // DTO class for use when recieving Contact update requests
    public class UpdateContactRequest : DetailedContact
    {
        [Required]
        public int? CategoryId { get; set; }

        [PasswordValidator(IsEdit = true)]
        public string? Password { get; set; }
    }
}
