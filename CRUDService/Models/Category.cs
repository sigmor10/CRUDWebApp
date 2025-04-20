using System.ComponentModel.DataAnnotations;

namespace CRUDService.Models
{
    // Entity class for Dcitionary table Categories
    public class Category
    {
        public required int Id { get; set; }

        public required string Name { get; set; }
    }
}
