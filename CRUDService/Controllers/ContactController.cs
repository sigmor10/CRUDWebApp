using CRUDService.DTOs;
using CRUDService.Helpers;
using CRUDService.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CRUDService.Controllers
{
    // Controller class which handles http(s) communication,
    // through the use of REST API.
    // It only handles requests/responses, actual operations are relayed to service layer.
    [ApiController]
    [Route("api")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        private readonly ILogger<ContactController> _logger;

        public ContactController(
            ILogger<ContactController> logger,
            IContactService service
            )
        {
            _logger = logger;
            _contactService = service;
        }

        // Returns paginated list of Contacts
        [HttpGet("contacts/{skip}/{take}")]
        public async Task<IActionResult> GetContacts(int skip, int take)
        {
            var contacts = await _contactService.FindAllContacts(skip,take);
            return Ok(ContactMapper.ToContactsResponse(contacts));    
        }

        // Returns a detailed Contact with given id
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

        // Returns list of all categories
        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            return Ok( await _contactService.FindAllCategories());
        }

        // Returns list of anmes of all subcategories for given category id
        [HttpGet("subcategories/{id}")]
        public async Task<IActionResult> GetSubCategories(int id)
        {
            return Ok(await _contactService.FindAllSubCategoriesById(id));
        }

        // Returns list of anmes of all subcategories
        [HttpGet("subcategories")]
        public async Task<IActionResult> GetSubAllCategories()
        {
            return Ok(await _contactService.FindAllSubCategories());
        }

        // Returns information if email is taken or not
        [HttpGet("email")]
        public async Task<IActionResult> CheckIfEmailTaken(
            [FromQuery] string email
            )
        {
            return Ok(await _contactService.CheckIfEmailTaken(email));
        }

        // Creates new Contact instance
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

        // Updates existing Contact instance
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

        // Deletes existing Contact
        [Authorize]
        [HttpDelete("contacts/{id}/delete")]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            if(await _contactService.DeleteContact(id)) return Ok();

            return NotFound();
        }
    }
}
