namespace CRUDService.Repository
{
    public interface IUserRepository
    {
        // Retrieves password hash for given email
        Task<string?> FindUserPasswordHash(string email);
    }
}
