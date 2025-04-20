using System.ComponentModel.DataAnnotations;

namespace CRUDService.Models
{
    /// <summary>
    /// Entity class for Dcitionary table Categories
    /// </summary>
    public class Category
    {
        public required int Id { get; set; }

        public required string Name { get; set; }
    }
}
