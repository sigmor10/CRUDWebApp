using CRUDService.Data;
using Microsoft.EntityFrameworkCore;

namespace CRUDService.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository (AppDbContext context)
        {
            _context = context;
        }

        public async Task<string?> FindUserPasswordHash(string email)
        {
            return await _context.Contacts
                .Where(c => c.Email == email)
                .Select(c => c.PasswordHash)
                .FirstOrDefaultAsync();
        }
    }
}
