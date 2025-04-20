using System.ComponentModel.DataAnnotations;

namespace CRUDService.DTOs
{
    // Detailed DTO class used to transfer data needed for detailed view of contact info.
    // Caution: Password field is purposefully omitted due to security reasons.
    public class GetContactResponse : DetailedContactDTO
    {
        [Required]
        public required string Category { get; set; }
    }
}
