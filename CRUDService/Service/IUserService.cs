using CRUDService.Models;

namespace CRUDService.Service
{
    public interface IUserService
    {
        // Retrieves password hash for given email
        Task<string?> FindUserPasswordHash(string email);

        // Hadnles suer authentication
        Task<string?> Authenticate(User user);

        // Verifies given password with the one stored in database
        Task<bool> VerifyPassword(string givenHash, string email);

        // Generates jwt token for authorization
        string GenerateJwtToken(string email);
    }
}
