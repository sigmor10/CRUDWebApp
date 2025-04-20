using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    // Entity class for Dictionary entity SubCategory
    // it is used only for dictionary entries For SubCategory that are limited to bussiness Category
    // For database table constraint details see Contracts/SubCategoryConfiguration.cs file
    public class SubCategory
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required int CategoryId { get; set; }
    }
}
