using System.ComponentModel.DataAnnotations;

namespace CRUDService.DTOs
{
    /// <summary>
    /// Detailed DTO class used to transfer data needed for detailed view of contact info.
    /// Caution: Password field is purposefully omitted due to security reasons.
    /// Annotated for early valdiation and early rejection of invalid requests.
    /// </summary>
    public class GetContactResponse : DetailedContactDTO
    {
        [Required]
        public required string Category { get; set; }
    }
}
