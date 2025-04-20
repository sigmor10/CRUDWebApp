using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    // Detailed DTO class used to transfer data needed for detailed view of contact info.
    // Caution: Password field is purposefully omitted due to security reasons.
    public class GetContactResponse : DetailedContact
    {
        [Required]
        public string? Category { get; set; }
    }
}
