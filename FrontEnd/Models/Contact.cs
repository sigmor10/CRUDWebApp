using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    //Base DTO holds every field that is used in every other contact DTO class
    public class Contact
    {
        public Guid Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Surname { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
