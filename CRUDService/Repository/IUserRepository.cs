namespace CRUDService.Repository
{
    /// <summary>
    /// Interface posseses collection of methods for handling data stored in the database
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Retrieves password hash for given email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Hash of a passwrod associated with given email</returns>
        Task<string?> FindUserPasswordHash(string email);
    }
}
