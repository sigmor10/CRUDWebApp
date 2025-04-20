using CRUDService.Data;
using CRUDService.DTOs;
using CRUDService.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDService.Repository
{
    // Implements repository logic related to access to the database
    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext _context;

        public ContactRepository (AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckIfIdExists(Guid id)
        {
            return await _context.Contacts.AnyAsync(c => c.Id == id);
        }

        public async Task<Contact?> FindContactById(Guid id)
        {
            return await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Contact>> FindAllContacts(int skip, int take)
        {
            return await _context.Contacts.Skip(skip).Take(take).ToListAsync();
        }

        public async Task<List<Category>> FindAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<string?> FindCategoryNameById(int id)
        {
            return await _context.Categories
                .Where(c => c.Id == id)
                .Select(c => c.Name)
                .FirstOrDefaultAsync();

        }

        public async Task<string?> FindUserEmailById(Guid id)
        {
            return await _context.Contacts
                .Where(c => c.Id == id)
                .Select(c => c.Email)
                .FirstOrDefaultAsync();
        }

        public async Task<List<string>> FindAllSubCategoriesById(int id)
        {
            return await _context
                .SubCategories
                .Where(c => c.CategoryId == id)
                .Select(c => c.Name)
                .ToListAsync();
        }

        public async Task AddContact(Contact contact)
        {
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateContact(Contact contact)
        {
            // Fetching existing contact record that is to be updated
            var existingContact = await _context.Contacts
                .FirstOrDefaultAsync(c => c.Id == contact.Id);

            if (existingContact == null) return false;
            if (await CheckIfEmailTaken(contact.Email) && 
                contact.Email != existingContact.Email) 
                return false;

                // Manual property updates
            existingContact.Name = contact.Name;
            existingContact.Surname = contact.Surname;
            existingContact.Email = contact.Email;
            existingContact.Phone = contact.Phone;
            existingContact.SubCategory = contact.SubCategory;
            existingContact.CategoryId = contact.CategoryId;

            // Password hash is only updated when its changed
            if (!string.IsNullOrWhiteSpace(contact.PasswordHash))
                existingContact.PasswordHash = contact.PasswordHash;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteContact(Guid id)
        {
            // Fetches existing contatc record for deletion
            var existingContact = await _context.Contacts
                .FirstOrDefaultAsync(c => c.Id == id);

            if (existingContact == null) return false;

            _context.Contacts.Remove(existingContact);
            await _context.SaveChangesAsync();

            return true;
        }

        public Task<bool> CheckIfEmailTaken(string email)
        {
            return _context.Contacts.AnyAsync(c => c.Email == email);
        }

        public async Task<List<SubCategory>> FindAllSubCategories()
        {
            return await _context.SubCategories.ToListAsync();
        }
    }
}
