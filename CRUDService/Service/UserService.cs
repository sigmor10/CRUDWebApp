
using CRUDService.Helpers;
using CRUDService.Models;
using CRUDService.Repository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CRUDService.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IConfiguration _config;

        public UserService(
            IUserRepository userRepo,
            IConfiguration config)
        {
            _userRepo = userRepo;
            _config = config;
        }

        public async Task<string?> Authenticate(User user)
        {
            var hash = HelperMethods.HashPassword(user.Password);

            if(!await VerifyPassword(hash, user.Email)) return null;

            return GenerateJwtToken(user.Email);
        }

        public async Task<string?> FindUserPasswordHash(string email)
        {
            return await _userRepo.FindUserPasswordHash(email);
        }

        public async Task<bool> VerifyPassword(string givenHash, string email)
        {
            string storedHash = await FindUserPasswordHash(email);
            if (storedHash == null) return false;

            return storedHash.Equals(givenHash);
        }

        public string GenerateJwtToken(string email)
        {
            // Fetch key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Generate token claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.Email, email),
            };

            // Generate token contents
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: creds
            );

            // Generate final token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
