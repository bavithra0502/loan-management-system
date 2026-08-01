using LoanManagementAPI.Models;
using LoanManagementAPI.Repositories;

namespace LoanManagementAPI.Services
{
    public class LoanVerificationService : ILoanVerificationService
    {
        private readonly IRepository<LoanVerification> _repository;
        private readonly ILoanRequestRepository _loanRequestRepository;

        public LoanVerificationService(
            IRepository<LoanVerification> repository,
            ILoanRequestRepository loanRequestRepository)
        {
            _repository = repository;
            _loanRequestRepository = loanRequestRepository;
        }

        public async Task<IEnumerable<LoanVerification>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<LoanVerification?> GetByIdAsync(int loanVerificationId) =>
            await _repository.GetByIdAsync(loanVerificationId);

        public async Task<IEnumerable<LoanVerification>> GetByOfficerIdAsync(int officerId) =>
            await _repository.FindAsync(lv => lv.OfficerId == officerId);

        public async Task<LoanVerification> AssignAsync(int loanRequestId, int officerId)
        {
            var verification = new LoanVerification
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

        // When the officer finalizes the loan verification, the parent
        // LoanRequest's status is kept in sync automatically.
        public async Task<bool> UpdateAsync(int loanVerificationId, string verificationResult, string status, string remarks)
        {
            var verification = await _repository.GetByIdAsync(loanVerificationId);
            if (verification == null) return false;

            verification.VerificationResult = verificationResult;
            verification.Status = status;
            verification.Remarks = remarks;
            verification.VerificationDate = DateTime.Now;
            _repository.Update(verification);

            if (status == "Approved" || status == "Rejected")
            {
                var loanRequest = await _loanRequestRepository.GetByIdAsync(verification.LoanRequestId);
                if (loanRequest != null)
                {
                    loanRequest.Status = status;
                    _loanRequestRepository.Update(loanRequest);
                }
            }

            return await _repository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int loanVerificationId)
        {
            var verification = await _repository.GetByIdAsync(loanVerificationId);
            if (verification == null) return false;

            _repository.Delete(verification);
            return await _repository.SaveChangesAsync();
        }
    }
}
