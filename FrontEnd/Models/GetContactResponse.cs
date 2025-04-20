using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    /// <summary>
    /// Detailed DTO class used to transfer data needed for detailed view of contact info.
    /// Caution: Password field is purposefully omitted due to security reasons.
    /// Annotated for early valdiation and early rejection of invalid requests.
    /// </summary>
    public class GetContactResponse : DetailedContact
    {
        [Required]
        public string? Category { get; set; }
    }
}
