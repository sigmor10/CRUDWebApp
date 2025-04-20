using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    //More detailed DTO used for adding missing fields from BASEContactDTO.
    // It's used to minimize code duplication.
    public class DetailedContact : Contact
    {
        public string? SubCategory { get; set; }

        [Required]
        [Phone]
        public string? Phone { get; set; }

        [Required]
        public DateOnly BirthDate { get; set; }
    }
}
