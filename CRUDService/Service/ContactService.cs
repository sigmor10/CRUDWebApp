using CRUDService.Helpers;
using CRUDService.Models;
using CRUDService.Repository;

namespace CRUDService.Service
{
    /// <summary>
    /// Implements bussiness logic and mediates between contrler and repository layers
    /// </summary>
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepo;

        /// <summary>
        /// Constructor for supporting injections
        /// </summary>
        /// <param name="contactRepo">Injected ContactRepository object</param>
        public ContactService(IContactRepository contactRepo)
        {
            _contactRepo = contactRepo;
        }

        public async Task<bool> AddContact(Contact contact)
        {
            // Validates email and password complexity
            if (await _contactRepo.CheckIfEmailTaken(contact.Email)) return false;

            if (!await CommonContactValidation(contact)) return false;

            // Checks if id is already in use, if so, generates new ones untill it finds a free id
            while (await _contactRepo.CheckIfIdExists(contact.Id))
            {
                contact.Id = Guid.NewGuid();
            }

            // Hashes password for safe storage and adds entity
            contact.PasswordHash = HelperMethods.HashPassword(contact.PasswordHash);
            await _contactRepo.AddContact(contact);

            return true;
        }

        public async Task<bool> CheckIfEmailTaken(string email)
        {
           return await _contactRepo.CheckIfEmailTaken(email);
        }

        public async Task<bool> CheckIfIdExists(Guid id)
        {
            return await _contactRepo.CheckIfIdExists(id);
        }

        public async Task<bool> CommonContactValidation(Contact contact)
        {
            var categories = await _contactRepo.FindAllCategories();

            // Checks validity of most properties
            var result = HelperMethods.ValidateProperties(contact,
                categories.Select(c => c.Id).ToList(),
                await _contactRepo.FindAllSubCategoriesById(contact.CategoryId));

            return result;
        }

        public async Task<bool> DeleteContact(Guid id)
        {
            return await _contactRepo.DeleteContact(id);
        }

        public async Task<List<Category>> FindAllCategories()
        {
            return await _contactRepo.FindAllCategories();
        }

        public async Task<List<Contact>> FindAllContacts(int skip, int take)
        {
            return await _contactRepo.FindAllContacts(skip, take);
        }

        public async Task<List<SubCategory>> FindAllSubCategories()
        {
            return await _contactRepo.FindAllSubCategories();
        }

        public async Task<List<string>> FindAllSubCategoriesById(int id)
        {
            return await _contactRepo.FindAllSubCategoriesById(id);
        }

        public async Task<string?> FindCategoryNameById(int id)
        {
            return await _contactRepo.FindCategoryNameById(id);
        }

        public async Task<Contact?> FindContactById(Guid id)
        {
            return await _contactRepo.FindContactById(id);
        }

        public async Task<string?> FindUserEmailById(Guid id)
        {
            return await _contactRepo.FindUserEmailById(id);
        }

        public async Task<bool> UpdateContact(Contact contact)
        {
            if(!await _contactRepo.CheckIfIdExists(contact.Id)) return false;

            if (!await CommonContactValidation(contact)) return false;

            if(!string.IsNullOrWhiteSpace(contact.PasswordHash))
                contact.PasswordHash = HelperMethods.HashPassword(contact.PasswordHash);

            return await _contactRepo.UpdateContact(contact);
        }
    }
}
