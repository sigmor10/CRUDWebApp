using CRUDService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRUDService.Contracts
{
    /// <summary>
    /// Class needed for configuring specific rules of Contacts table in the database
    /// </summary>
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        /// <summary>
        /// Sets configuration of the Contacts table
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            // Setting PK and required constraints on fields
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).IsRequired();

            builder.Property(c => c.Surname).IsRequired();

            builder.Property(c => c.BirthDate).IsRequired();

            builder.Property(c => c.Email).IsRequired();

            builder.HasIndex(c => c.Email).IsUnique();

            // Declaring contact's subcategory as not required and limiting its length
            builder.Property(c => c.SubCategory).HasMaxLength(40).IsRequired(false);

            // Declaring id of category as FK without relationship navigation mapping
            builder.HasOne<Category>()
                .WithMany()
                .HasForeignKey(c => c.CategoryId)
                .IsRequired();
        }
    }
}
