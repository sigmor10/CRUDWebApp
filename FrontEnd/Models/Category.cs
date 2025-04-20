using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    /// <summary>
    /// Entity class for Dictionary entity Category
    /// For database table constraint details see Contracts/CategoryConfiguration.cs file
    /// Annotated for early valdiation and early rejection of invalid requests.
    /// </summary>
    public class Category
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }
    }
}
