using CRUDService.DTOs;
using CRUDService.Helpers;
using CRUDService.Models;
using CRUDService.Service;
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
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// Propery used to access User Service
        /// </summary>
        private readonly IUserService _userService;

        private readonly ILogger<ContactController> _logger;

        /// <summary>
        /// Constructor with dependency injections
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="service"></param>
        public AuthController(
            ILogger<ContactController> logger,
            IUserService service
            )
        {
            _logger = logger;
            _userService = service;
        }

        /// <summary>
        /// Returns paginated list of Contacts
        /// </summary>
        /// <param name="user">User object passed for use of in service layer</param>
        /// <returns>Jwt Token or 401 Unauthorized message</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Authorize([FromBody] User user)
        {
            var token = await _userService.Authenticate(user);

            if (token == null) return Unauthorized();
            else return Ok(token);
        }
    }
}
