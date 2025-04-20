using System.ComponentModel.DataAnnotations;

namespace CRUDService.DTOs
{
    /// <summary>
    /// Base DTO holds every field that is used in every other contact DTO class
    /// Annotated for early valdiation and early rejection of invalid requests.
    /// </summary>
    public class BaseContactDTO
    {
        public Guid Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Surname { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }
    }
}
