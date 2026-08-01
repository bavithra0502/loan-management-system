using LoanManagementAPI.Models;

namespace LoanManagementAPI.Repositories
{
    public interface ILoanRequestRepository : IRepository<LoanRequest>
    {
        Task<IEnumerable<LoanRequest>> GetByCustomerIdAsync(int customerId);
        Task<LoanRequest?> GetByIdWithDetailsAsync(int loanRequestId);
    }
}
