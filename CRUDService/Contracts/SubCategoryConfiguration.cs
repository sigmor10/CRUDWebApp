﻿using CRUDService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRUDService.Contracts
{
    /// <summary>
    /// Class needed for configuring specific rules of its table in the db
    /// </summary>
    public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        /// <summary>
        /// Sets configuration of the Contacts table
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            // Declaring id as PK and setting it to auto-increment on database side
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            // Setting unique constraint on Name column
            builder.Property(c => c.Name).IsRequired();
            builder.HasIndex(c => c.Name).IsUnique();

            // Declaring id of category as FK without relationship navigation mapping
            builder.HasOne<Category>()
                .WithMany()
                .HasForeignKey(c => c.CategoryId)
                .IsRequired();
        }
    }
}
