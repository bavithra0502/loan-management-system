using LoanManagementAPI.Models;

namespace LoanManagementAPI.Services
{
    public interface ILoanVerificationService
    {
        Task<IEnumerable<LoanVerification>> GetAllAsync();
        Task<LoanVerification?> GetByIdAsync(int loanVerificationId);
        Task<IEnumerable<LoanVerification>> GetByOfficerIdAsync(int officerId);
        Task<LoanVerification> AssignAsync(int loanRequestId, int officerId);
        Task<bool> UpdateAsync(int loanVerificationId, string verificationResult, string status, string remarks);
        Task<bool> DeleteAsync(int loanVerificationId);
    }
}
