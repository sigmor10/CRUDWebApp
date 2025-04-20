using CRUDService.Contracts;
using CRUDService.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDService.Data
{
    /// <summary>
    /// Class used to manage the connection to the database.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<SubCategory> SubCategories { get; set; }

        /// <summary>
        /// Applies custom rules to properties of columns that are based on class fields
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Contact table
            modelBuilder.ApplyConfiguration(new ContactConfiguration());

            // Category dictionary table
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());

            // SubCategory dictionary table
            modelBuilder.ApplyConfiguration(new SubCategoryConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
