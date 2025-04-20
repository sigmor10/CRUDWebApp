using System.ComponentModel.DataAnnotations;

namespace CRUDService.DTOs
{
    //More detailed DTO used for adding missing fields from BASEContactDTO.
    // It's used to minimize code duplication.
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
