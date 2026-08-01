using LoanManagementAPI.Models;

namespace LoanManagementAPI.Services
{
    public interface IBackgroundVerificationService
    {
        Task<IEnumerable<BackgroundVerification>> GetAllAsync();
        Task<BackgroundVerification?> GetByIdAsync(int verificationId);
        Task<IEnumerable<BackgroundVerification>> GetByOfficerIdAsync(int officerId);
        Task<BackgroundVerification> AssignAsync(int loanRequestId, int officerId);
        Task<bool> UpdateAsync(int verificationId, string status, string remarks);
        Task<bool> DeleteAsync(int verificationId);
    }
}
