using CRUDService.DTOs;
using CRUDService.Models;

namespace CRUDService.Helpers
{
    // Class contains methods used to transform Contact entity to and from DTO objects
    public static class ContactMapper
    {
        // Transforms Contact entity into a simplified DTO object
        public static BaseContactDTO ToBaseContactDTO(Contact contact)
        {
            return new BaseContactDTO
            {
                Id = contact.Id,
                Email = contact.Email,
                Name = contact.Name,
                Surname = contact.Surname
            };
        }

        // Transforms lsit of Contact entity objects into a list of BaseContactDTO objects
        public static List<BaseContactDTO> ToContactsResponse(List<Contact> contacts)
        {
            return contacts.Select(ToBaseContactDTO).ToList();
        }

        // Transforms Contact entity into a detailed DTO onject
        public static GetContactResponse ToContactResponse(Contact contact, string categoryName)
        {
            return new GetContactResponse
            {
                Id = contact.Id,
                Name = contact.Name,
                Surname = contact.Surname,
                Email = contact.Email,
                Category = categoryName,
                SubCategory = contact.SubCategory,
                Phone = contact.Phone,
                BirthDate = contact.BirthDate,
            };
        }

        // Creates Contact entity from a CreateContactRequest DTO
        public static Contact ToContact(CreateContactRequest request)
        {
            return new Contact
            {
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                PasswordHash = request.Password,
                Phone = request.Phone,
                BirthDate = request.BirthDate,
                CategoryId = request.CategoryId,
                SubCategory = request.SubCategory
            };
        }

        // Creates Contact entity from a UpdateContactRequest DTO
        public static Contact UpdateRequestToContact(UpdateContactRequest request)
        {
            return new Contact
            {
                Id = request.Id,
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                PasswordHash = request.Password,
                Phone = request.Phone,
                BirthDate = request.BirthDate,
                CategoryId = request.CategoryId,
                SubCategory = request.SubCategory
            };
        }
    }
}
