using CRUDService.Models;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace CRUDService.Helpers
{
    /// <summary>
    /// Collection of helper methods
    /// </summary>
    public static class HelperMethods
    {
        /// <summary>
        /// Validates common properties
        /// </summary>
        /// <param name="contact">Contact object to be validated</param>
        /// <param name="categories">List of category ids</param>
        /// <param name="subCategories">List of subcategory names for given category</param>
        /// <returns>true if properties are valid, otherwise false</returns>
        public static bool ValidateProperties(
            Contact contact, 
            List<int> categories,
            List<string> subCategories)
        {
            // Validates category
            if (!categories.Contains(contact.CategoryId)) return false;

            // Validates subcategory
            if (!ValidateSubCategory(
                contact.SubCategory,
                subCategories))
                return false;

            if (string.IsNullOrEmpty(contact.Name) || string.IsNullOrEmpty(contact.Surname))
                return false;

            return true;
        }

        /// <summary>
        /// Hashes the password for storage
        /// </summary>
        /// <param name="password">Password to be hashed</param>
        /// <returns>Hashed password as string</returns>
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                // Converts the password to bytes
                var bytes = Encoding.UTF8.GetBytes(password);

                // Computes the hash
                var hashBytes = sha256.ComputeHash(bytes);

                // Converts the hash bytes to a hexadecimal string
                var hashString = BitConverter
                    .ToString(hashBytes)
                    .Replace("-", "")
                    .ToLowerInvariant();

                return hashString;
            }
        }

        /// <summary>
        /// Validates password according to common rules
        /// </summary>
        /// <param name="password">Password to be validated</param>
        /// <returns>bool saying whether password is valid or not</returns>
        public static bool ValidatePassword(string password)
        {
            // Check if password is empty, null, or comprised solely of whitespace chars
            if (string.IsNullOrWhiteSpace(password)) return false;

            // check length
            if(password.Length < 8) return false;

            // Check if password follows common rules of complexicity
            bool hasUpper = password.Any(char.IsUpper); // at least one uppercase letter
            bool hasLower = password.Any(char.IsLower); // at least one lowercase letter
            bool hasDigit = password.Any(char.IsDigit); // at least one digit
            bool hasSpecial = password.Any(c => !char.IsLetterOrDigit(c)); // at least one special character

            return hasUpper && hasLower && hasDigit && hasSpecial;
        }

        /// <summary>
        /// Validates subcategory value
        /// </summary>
        /// <param name="subCategory">Name of a subcategory</param>
        /// <param name="categories">List of subcategories names</param>
        /// <returns></returns>
        public static bool ValidateSubCategory(
            string subCategory, 
            List<string> categories)
        {
            if (categories.Count == 0) return true; // Category has no fixed subcategories
            else return categories.Contains(subCategory); // Checks if subcategory is among fixed subcategories
        }
    }
}
