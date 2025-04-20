using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    // Entity class for Dictionary entity Category
    // For database table constraint details see Contracts/CategoryConfiguration.cs file
    public class Category
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }
    }
}
