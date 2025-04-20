using CRUDService.DTOs;
using CRUDService.Helpers;
using CRUDService.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CRUDService.Controllers
{
    /// <summary>
    /// Controller class which handles http(s) communication,
    /// through the use of REST API.
    /// It only handles requests/responses, actual operations are relayed to service layer.
    /// </summary>
    [ApiController]
    [Route("api")]
    public class ContactController : ControllerBase
    {
        /// <summary>
        /// Propery used to access Contact Service
        /// </summary>
        private readonly IContactService _contactService;

        private readonly ILogger<ContactController> _logger;

        /// <summary>
        /// Constructor with dependency injections
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="service"></param>
        public ContactController(
            ILogger<ContactController> logger,
            IContactService service
            )
        {
            _logger = logger;
            _contactService = service;
        }

        /// <summary>
        /// Returns paginated list of Contacts
        /// </summary>
        /// <param name="skip">Defines how many records should be skipped</param>
        /// <param name="take">Defines max length of returned list</param>
        /// <returns>Returns list Of BaseContactDTO objects, can be empty</returns>
        [HttpGet("contacts/{skip}/{take}")]
        public async Task<IActionResult> GetContacts(int skip, int take)
        {
            var contacts = await _contactService.FindAllContacts(skip,take);
            return Ok(ContactMapper.ToContactsResponse(contacts));    
        }

        /// <summary>
        /// Returns a detailed Contact with given id
        /// </summary>
        /// <param name="id">Guid of the requested contact</param>
        /// <returns>404 or requested Contact transformed into GetContactResponse Object</returns>
        [HttpGet("contacts/{id}")]
        public async Task<IActionResult> GetContact(Guid id)
        {
            // Fetching existing Contact
            var contact = await _contactService.FindContactById(id);
            if (contact == null) return NotFound(); // No contact with given id
                
            // Fetching name of contacts category
            var category = await _contactService.FindCategoryNameById(contact.CategoryId);
            if (category == null) return NotFound(); // No such category with id in the Contact

            return Ok(ContactMapper.ToContactResponse(contact, category));
        }

        /// <summary>
        /// Returns list of all categories
        /// </summary>
        /// <returns>List of all categories in the database</returns>
        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            return Ok( await _contactService.FindAllCategories());
        }

        /// <summary>
        /// Returns list of anmes of all subcategories for given category id
        /// </summary>
        /// <param name="id">Id of the category, requested subcategories belongg to.</param>
        /// <returns>List of all subcategory names that belong to Category with given id</returns>
        [HttpGet("subcategories/{id}")]
        public async Task<IActionResult> GetSubCategories(int id)
        {
            return Ok(await _contactService.FindAllSubCategoriesById(id));
        }

        /// <summary>
        /// Returns list of anmes of all subcategories
        /// </summary>
        /// <returns>list of all subcategoires in the database</returns>
        [HttpGet("subcategories")]
        public async Task<IActionResult> GetSubAllCategories()
        {
            return Ok(await _contactService.FindAllSubCategories());
        }

        /// <summary>
        /// Returns information if email is taken or not
        /// </summary>
        /// <param name="email">Email that is to be checked</param>
        /// <returns>bool saying whether given email already exists in database or not</returns>
        [HttpGet("email")]
        public async Task<IActionResult> CheckIfEmailTaken(
            [FromQuery] string email
            )
        {
            return Ok(await _contactService.CheckIfEmailTaken(email));
        }

        /// <summary>
        /// Creates new Contact instance
        /// </summary>
        /// <param name="request">CreateContactRequest object with data needed to create neew Contact</param>
        /// <returns>201 code or 400, depending on whether entry was created or not</returns>
        [Authorize]
        [HttpPost("contacts/add")]
        public async Task<IActionResult> CreateContact(
            [FromBody] CreateContactRequest request)
        {
            bool result = await _contactService
                .AddContact(ContactMapper.ToContact(request));

            // Checks if Contact was added
            if (result) return Created();
            return BadRequest(new { message = "Creation failed!!! Invalid parameters." });
        }

        /// <summary>
        /// Updates existing Contact instance
        /// </summary>
        /// <param name="id">Contact's Guid</param>
        /// <param name="request">UpdateContactRequest object containing new/updated data for existing Contact</param>
        /// <returns> following http codes:
        /// 404 if no contact exists with given id,
        /// 204 if update was successful
        /// 400 if data in UpdateContactRequest was invalid
        /// </returns>
        [Authorize]
        [HttpPatch("contacts/{id}/update")]
        public async Task<IActionResult> UpdateContact(
            Guid id, 
            [FromBody] UpdateContactRequest request)
        {
            if(!await _contactService.CheckIfIdExists(id)) return NotFound();

            bool result = await _contactService
                .UpdateContact(ContactMapper.UpdateRequestToContact(request));

            // Checks if Contact was updated
            if (result) return NoContent(); 
            else return BadRequest(new {message = "Update failed!!! Invalid parameters." });
        }

        /// <summary>
        /// Deletes existing Contact
        /// </summary>
        /// <param name="id">Contact's Guid</param>
        /// <returns>
        /// 404 code if Contact with given id does not exist
        /// 200 code if Contact was sucessfully deleted
        /// </returns>
        [Authorize]
        [HttpDelete("contacts/{id}/delete")]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            if(await _contactService.DeleteContact(id)) return Ok();

            return NotFound();
        }
    }
}
