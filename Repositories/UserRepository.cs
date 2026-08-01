using LoanManagementAPI.Data;
using LoanManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LoanManagementAPI.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(LoanManagementDbContext context) : base(context) { }

        public async Task<User?> GetByUserNameAsync(string userName) =>
            await _dbSet.FirstOrDefaultAsync(u => u.UserName == userName);

        public async Task<User?> GetByUserNameAndPasswordAsync(string userName, string password) =>
            await _dbSet.FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password);
    }
}
