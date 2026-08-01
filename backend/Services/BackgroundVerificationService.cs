using LoanManagementAPI.Models;
using LoanManagementAPI.Repositories;

namespace LoanManagementAPI.Services
{
    public class BackgroundVerificationService : IBackgroundVerificationService
    {
        private readonly IRepository<BackgroundVerification> _repository;

        public BackgroundVerificationService(IRepository<BackgroundVerification> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BackgroundVerification>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<BackgroundVerification?> GetByIdAsync(int verificationId) =>
            await _repository.GetByIdAsync(verificationId);

        public async Task<IEnumerable<BackgroundVerification>> GetByOfficerIdAsync(int officerId) =>
            await _repository.FindAsync(bv => bv.OfficerId == officerId);

        public async Task<BackgroundVerification> AssignAsync(int loanRequestId, int officerId)
        {
            var verification = new BackgroundVerification
            {
                LoanRequestId = loanRequestId,
                OfficerId = officerId,
                VerificationDate = DateTime.Now,
                Status = "Pending"
            };

            await _repository.AddAsync(verification);
            await _repository.SaveChangesAsync();
            return verification;
        }

        public async Task<bool> UpdateAsync(int verificationId, string status, string remarks)
        {
            var verification = await _repository.GetByIdAsync(verificationId);
            if (verification == null) return false;

            verification.Status = status;
            verification.Remarks = remarks;
            verification.VerificationDate = DateTime.Now;

            _repository.Update(verification);
            return await _repository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int verificationId)
        {
            var verification = await _repository.GetByIdAsync(verificationId);
            if (verification == null) return false;

            _repository.Delete(verification);
            return await _repository.SaveChangesAsync();
        }
    }
}
