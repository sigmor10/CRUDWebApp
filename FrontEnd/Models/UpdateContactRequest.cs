using FrontEnd.Validators;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    /// <summary>
    /// DTO class for use when recieving Contact update requests
    /// Annotated for early valdiation and early rejection of invalid requests.
    /// </summary>
    public class UpdateContactRequest : DetailedContact
    {
        [Required]
        public int? CategoryId { get; set; }

        [PasswordValidator(IsEdit = true)]
        public string? Password { get; set; }
    }
}
