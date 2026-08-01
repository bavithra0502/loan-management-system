using LoanManagementAPI.Models;

namespace LoanManagementAPI.Services
{
    public interface ILoanRequestService
    {
        Task<IEnumerable<LoanRequest>> GetAllAsync();
        Task<LoanRequest?> GetByIdAsync(int loanRequestId);
        Task<IEnumerable<LoanRequest>> GetByCustomerIdAsync(int customerId);
        Task<LoanRequest> ApplyAsync(LoanRequest loanRequest);
        Task<bool> UpdateStatusAsync(int loanRequestId, string status);
    }
}
