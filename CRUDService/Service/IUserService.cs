using CRUDService.Models;

namespace CRUDService.Service
{
    /// <summary>
    /// Defines bussiness logic in regards authentication of users 
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Retrieves password hash for given email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Hashed password from the database for the given email or null</returns>
        Task<string?> FindUserPasswordHash(string email);

        /// <summary>
        /// Hadnles user authentication
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns>Returns Jwttoken as string or null</returns>
        Task<string?> Authenticate(User user);

        /// <summary>
        /// Verifies given password with the one stored in database
        /// </summary>
        /// <param name="givenHash">Hshed password</param>
        /// <param name="email"></param>
        /// <returns>bool sqying whether givenHash is the same as the one stored in the databse for the given email</returns>
        Task<bool> VerifyPassword(string givenHash, string email);

        /// <summary>
        /// Generates jwt token for authorization
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Jwt token as string</returns>
        string GenerateJwtToken(string email);
    }
}
