using CRUDService.Models;

namespace CRUDService.Service
{
    // Defines bussiness logic operations for Contact management
    public interface IContactService
    {
        // Retrieves contact by its Email
        Task<bool> CheckIfEmailTaken(string email);

        // Checks if contact with given id exists
        Task<bool> CheckIfIdExists(Guid id);

        // Retrieves contact by its id
        Task<Contact?> FindContactById(Guid id);

        // Retrieves a paginated list of contacts
        Task<List<Contact>> FindAllContacts(int skip, int take);

        // Retrieves all categories
        Task<List<Category>> FindAllCategories();

        // Retrieves a category name for given id
        Task<string?> FindCategoryNameById(int id);

        // Retrieves user email by her/his id
        Task<string?> FindUserEmailById(Guid id);

        // Fetches all subcategoires by category id
        Task<List<string>> FindAllSubCategoriesById(int id);

        // Fetches all subcategoires
        Task<List<SubCategory>> FindAllSubCategories();

        // Adds new contact
        Task<bool> AddContact(Contact contact);

        // Updates existing contact
        Task<bool> UpdateContact(Contact contact);

        // Deletes existing contact
        Task<bool> DeleteContact(Guid id);

        // Performs validations for update and creation of contact
        Task<bool> CommonContactValidation(Contact contact);
    }
}
