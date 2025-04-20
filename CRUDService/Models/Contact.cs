using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDService.Models
{
    // Entity class for CRUD entity Contact
    // For database table constraint details see Contracts/ContactConfiguration.cs file
    public class Contact
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public required string Name { get; set; }

        public required string Surname { get; set; }

        public required string Email { get; set; }

        public string? PasswordHash { get; set; }

        public required int CategoryId { get; set; }

        public string? SubCategory { get; set; }

        public required string Phone { get; set; }

        public required DateOnly BirthDate { get; set; }
    }
}
