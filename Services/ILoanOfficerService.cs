using LoanManagementAPI.Models;

namespace LoanManagementAPI.Services
{
    public interface ILoanOfficerService
    {
        Task<IEnumerable<LoanOfficer>> GetAllAsync();
        Task<LoanOfficer?> GetByIdAsync(int officerId);
        Task<LoanOfficer?> GetByUserIdAsync(int userId);
        Task<bool> UpdateAsync(LoanOfficer officer);
    }
}
