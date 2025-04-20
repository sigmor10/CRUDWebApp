namespace CRUDService.Models
{
    /// <summary>
    /// Entity class for Dictionary entity SubCategory
    /// it is used only for dictionary entries For SubCategory that are limited to bussiness Category
    /// For database table constraint details see Contracts/SubCategoryConfiguration.cs file
    /// </summary>
    public class SubCategory
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required int CategoryId { get; set; }
    }
}
