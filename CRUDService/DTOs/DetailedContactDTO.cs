using System.ComponentModel.DataAnnotations;

namespace CRUDService.DTOs
{
    /// <summary>
    /// More detailed DTO used for adding missing fields from BASEContactDTO.
    /// It's used to minimize code duplication.
    /// Annotated for early valdiation and early rejection of invalid requests.
    /// </summary>
    public class DetailedContactDTO : BaseContactDTO
    {
        public string? SubCategory { get; set; }

        [Required]
        [Phone]
        public required string Phone { get; set; }

        [Required]
        public required DateOnly BirthDate { get; set; }
    }
}
