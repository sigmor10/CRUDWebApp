using CRUDService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRUDService.Contracts
{
    // Class needed for configuring specific rules of its table in the db
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
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
