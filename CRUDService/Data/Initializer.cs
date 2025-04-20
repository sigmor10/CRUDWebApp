using CRUDService.Helpers;
using CRUDService.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDService.Data
{

    // Populates empty database tables
    public static class Initializer
    {
        public static async Task InitializeAsync(AppDbContext context)
        {
            // Apply any pending migrations
            await context.Database.MigrateAsync();

            // Seeds Categories if empty
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Id = 1, Name = "służbowy",},
                    new Category { Id = 2, Name = "prywatny"},
                    new Category { Id = 3, Name = "inny"}
                };

                context.Categories.AddRange(categories);
                await context.SaveChangesAsync();
            }

            await SeedSubCategoriesAsync(context);

            await SeedContactsAsync(context);
        }

        // Seeds SubCategories if empty
        public static async Task SeedSubCategoriesAsync(AppDbContext context)
        {
            if (!context.SubCategories.Any())
            {
                var subcategories = new List<SubCategory>
                {
                    new SubCategory { Name = "szef", CategoryId = 1},
                    new SubCategory { Name = "klient", CategoryId = 1},
                    new SubCategory { Name = "współpracownik", CategoryId = 1},
                    new SubCategory { Name = "kierownik", CategoryId = 1},
                    new SubCategory { Name = "asystent", CategoryId = 1},
                    new SubCategory { Name = "pracownik", CategoryId = 1},
                    new SubCategory { Name = "prezes", CategoryId = 1},
                    new SubCategory { Name = "", CategoryId = 2}
                };

                await context.SubCategories.AddRangeAsync(subcategories);
                await context.SaveChangesAsync();
            }
        }

        // Seeds Contacts if empty
        public static async Task SeedContactsAsync(AppDbContext context)
        {
            if (!context.Contacts.Any())
            {
                var contacts = new List<Contact>
                {
                    new Contact 
                    { 
                        Name = "Arek",
                        Surname = "Arwin",
                        Email = "aerk.arwiin@wp.pl",
                        PasswordHash = HelperMethods.HashPassword("HelloWorld!!!2"),
                        BirthDate = new DateOnly(2000, 11, 2),
                        Phone = "000-000-000",
                        CategoryId = 2,
                        SubCategory = ""
                    },
                    new Contact
                    {
                        Name = "Ela",
                        Surname = "Merlin",
                        Email = "avalon@o2.pl",
                        PasswordHash = HelperMethods.HashPassword("Hogwart56?"),
                        BirthDate = new DateOnly(1991, 3, 25),
                        Phone = "564-111-252",
                        CategoryId = 1,
                        SubCategory = "szef"
                    },
                    new Contact
                    {
                        Name = "Felix",
                        Surname = "Melix",
                        Email = "fmelix@gmail.com",
                        PasswordHash = HelperMethods.HashPassword("MariaDb20&&"),
                        BirthDate = new DateOnly(1975, 8, 13),
                        Phone = "000-000-000",
                        CategoryId = 3,
                        SubCategory = "misc"
                    }
                };

                await context.Contacts.AddRangeAsync(contacts);
                await context.SaveChangesAsync();
            }
        }
    }
}
