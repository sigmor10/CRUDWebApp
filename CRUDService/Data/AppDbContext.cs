using CRUDService.Contracts;
using CRUDService.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDService.Data
{
    // Class used to manage the connection to the database.
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<SubCategory> SubCategories { get; set; }

        // Applies custom rules to properties of columns that are based on class fields
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
