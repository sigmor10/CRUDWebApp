using CRUDService.Models;

namespace CRUDService.Service
{
    /// <summary>
    /// Defines bussiness logic operations for Contact management
    /// </summary>
    public interface IContactService
    {
        /// <summary>
        /// Retrieves contact by its Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>bool saying whether email exists or not</returns>
        Task<bool> CheckIfEmailTaken(string email);

        /// <summary>
        /// Checks if contact with given id exists
        /// </summary>
        /// <param name="id">Contact's Guid</param>
        /// <returns>bool saying whether object with given id exists or not</returns>
        Task<bool> CheckIfIdExists(Guid id);

        /// <summary>
        /// Retrieves contact by its id
        /// </summary>
        /// <param name="id">Contact's Guid</param>
        /// <returns>Contact object or null</returns>
        Task<Contact?> FindContactById(Guid id);

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
        /// <returns>List of Category objects in the database</returns>
        Task<List<Category>> FindAllCategories();

        /// <summary>
        /// Retrieves a category name for given id
        /// </summary>
        /// <param name="id">Id of Category object</param>
        /// <returns>List of names of all subcategories of category with given id</returns>
        Task<string?> FindCategoryNameById(int id);

        /// <summary>
        /// Retrieves user email by her/his id
        /// </summary>
        /// <param name="id">Contact's Guid</param>
        /// <returns>Email as string or null</returns>
        Task<string?> FindUserEmailById(Guid id);

        /// <summary>
        /// Fetches all subcategoires by category id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of names of all the subcategory names belonging to the given Category</returns>
        Task<List<string>> FindAllSubCategoriesById(int id);

        /// <summary>
        /// Fetches all subcategoires
        /// </summary>
        /// <returns>List of all SubCategory objects in the database</returns>
        Task<List<SubCategory>> FindAllSubCategories();

        /// <summary>
        /// Adds new contact
        /// </summary>
        /// <param name="contact">Contact object</param>
        /// <returns>bool whether Contact object was successfully added to the database</returns>
        Task<bool> AddContact(Contact contact);

        /// <summary>
        /// Updates existing contact
        /// </summary>
        /// <param name="contact">Contact object</param>
        /// <returns>bool saying whether update was successful or not</returns>
        Task<bool> UpdateContact(Contact contact);

        /// <summary>
        /// Deletes existing contact
        /// </summary>
        /// <param name="id">Contact's Guid</param>
        /// <returns>bool sqying whether deletion was successful or not</returns>
        Task<bool> DeleteContact(Guid id);

        /// <summary>
        /// Performs validations for update and creation of contact
        /// </summary>
        /// <param name="contact">Contact object</param>
        /// <returns>bool saying whether given Contact object's properties</returns>
        Task<bool> CommonContactValidation(Contact contact);
    }
}
