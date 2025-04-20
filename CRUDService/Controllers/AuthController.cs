using CRUDService.DTOs;
using CRUDService.Helpers;
using CRUDService.Models;
using CRUDService.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CRUDService.Controllers
{
    // Controller class which handles http(s) communication,
    // through the use of REST API.
    // It only handles requests/responses, actual operations are relayed to service layer.
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly ILogger<ContactController> _logger;

        public AuthController(
            ILogger<ContactController> logger,
            IUserService service
            )
        {
            _logger = logger;
            _userService = service;
        }

        // Returns paginated list of Contacts
        [HttpPost("login")]
        public async Task<IActionResult> GetContacts([FromBody] User user)
        {
            var token = await _userService.Authenticate(user);

            if (token == null) return Unauthorized();
            else return Ok(token);
        }
    }
}
