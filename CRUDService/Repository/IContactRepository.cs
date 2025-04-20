using CRUDService.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDService.Repository
{
    /// <summary>
    /// Interface posseses collection of methods for handling data stored in the database
    /// </summary>
    public interface IContactRepository
    {
        /// <summary>
        /// Checks if contact with given id exists
        /// </summary>
        /// <param name="id">Contacts Guid</param>
        /// <returns>true if exists false if not</returns>
        Task<bool> CheckIfIdExists(Guid id);

        /// <summary>
        /// Retrieves contact by its id
        /// </summary>
        /// <param name="id">Contacts Guid</param>
        /// <returns>Either Contact object or null</returns>
        Task<Contact?> FindContactById(Guid id);

        /// <summary>
        /// Retrieves contact by its Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>true if given email exists or flase if not</returns>
        Task<bool> CheckIfEmailTaken(string email);

        /// <summary>
        /// Retrieves a paginated list of contacts
        /// </summary>
        /// <param name="skip">Defines how many records should be skipped</param>
        /// <param name="take">Defines max length of returned list</param>
        /// <returns>Either Contact object or null</returns>
        Task<List<Contact>> FindAllContacts(int skip, int take);

        /// <summary>
        /// Retrieves all categories
        /// </summary>
        /// <returns>Category object</returns>
        Task<List<Category>> FindAllCategories();

        /// <summary>
        /// Retrieves a category name for given id
        /// </summary>
        /// <param name="id">Id of a Category</param>
        /// <returns>Category's name</returns>
        Task<string?> FindCategoryNameById(int id);

        /// <summary>
        /// Retrieves user email by her/his id
        /// </summary>
        /// <param name="id">Contact's Guid</param>
        /// <returns>Contact's email</returns>
        Task<string?> FindUserEmailById(Guid id);

        /// <summary>
        /// Fetches all subcategoires for given id
        /// </summary>
        /// <param name="id">Id of a Category</param>
        /// <returns>List of names of all the subcategory names belonging to the given Category</returns>
        Task<List<string>> FindAllSubCategoriesById(int id);

        /// <summary>
        /// Fetches all subcategoires
        /// </summary>
        /// <returns>List of all subcategories</returns>
        Task<List<SubCategory>> FindAllSubCategories();

        /// <summary>
        /// Adds new contact
        /// </summary>
        /// <param name="contact">Contact object</param>
        /// <returns></returns>
        Task AddContact(Contact contact);

        /// <summary>
        /// Updates existing contact
        /// </summary>
        /// <param name="contact">Contact object</param>
        /// <returns>true if object was updated successfully false if not</returns>
        Task<bool> UpdateContact(Contact contact);

        /// <summary>
        /// Deletes existing contact
        /// </summary>
        /// <param name="id">Contact's Guid</param>
        /// <returns>true if object was deleted successfully false if not</returns>
        Task<bool> DeleteContact(Guid id);
    }
}
