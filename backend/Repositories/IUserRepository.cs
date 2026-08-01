using LoanManagementAPI.Models;

namespace LoanManagementAPI.Repositories
{
    // Extra, entity-specific queries that don't belong on the generic repository
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByUserNameAsync(string userName);
        Task<User?> GetByUserNameAndPasswordAsync(string userName, string password);
    }
}
