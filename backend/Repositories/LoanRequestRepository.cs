using LoanManagementAPI.Data;
using LoanManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LoanManagementAPI.Repositories
{
    public class LoanRequestRepository : Repository<LoanRequest>, ILoanRequestRepository
    {
        public LoanRequestRepository(LoanManagementDbContext context) : base(context) { }

        public async Task<IEnumerable<LoanRequest>> GetByCustomerIdAsync(int customerId) =>
            await _dbSet.Where(lr => lr.CustomerId == customerId).ToListAsync();

        public async Task<LoanRequest?> GetByIdWithDetailsAsync(int loanRequestId) =>
            await _dbSet
                .Include(lr => lr.Customer)
                .Include(lr => lr.BackgroundVerification)
                .Include(lr => lr.LoanVerification)
                .FirstOrDefaultAsync(lr => lr.LoanRequestId == loanRequestId);
    }
}
