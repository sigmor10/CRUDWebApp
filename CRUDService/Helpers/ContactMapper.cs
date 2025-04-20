using CRUDService.DTOs;
using CRUDService.Models;

namespace CRUDService.Helpers
{
    /// <summary>
    /// Class contains methods used to transform Contact entity to and from DTO objects
    /// </summary>
    public static class ContactMapper
    {
        /// <summary>
        /// Transforms Contact entity into a simplified DTO object.
        /// </summary>
        /// <param name="contact">Contact object</param>
        /// <returns>BaseContactDTO object</returns>
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

        /// <summary>
        /// Transforms lsit of Contact entity objects into a list of BaseContactDTO objects
        /// </summary>
        /// <param name="contacts">Contact object</param>
        /// <returns>List of BaseContactDTO objects</returns>
        public static List<BaseContactDTO> ToContactsResponse(List<Contact> contacts)
        {
            return contacts.Select(ToBaseContactDTO).ToList();
        }

        /// <summary>
        /// Transforms Contact entity into a detailed DTO onject
        /// </summary>
        /// <param name="contact">Contact object</param>
        /// <param name="categoryName">Name of a category</param>
        /// <returns>GetContactResponse object</returns>
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

        /// <summary>
        /// Creates Contact entity from a CreateContactRequest DTO
        /// </summary>
        /// <param name="request">CreateContactRequest object</param>
        /// <returns>Contact object</returns>
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

        /// <summary>
        /// Creates Contact entity from a UpdateContactRequest DTO
        /// </summary>
        /// <param name="request">UpdateContactRequest object</param>
        /// <returns>Contact object</returns>
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
