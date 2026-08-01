using LoanManagementAPI.Models;
using LoanManagementAPI.Repositories;

namespace LoanManagementAPI.Services
{
    public class LoanRequestService : ILoanRequestService
    {
        private readonly ILoanRequestRepository _loanRequestRepository;

        public LoanRequestService(ILoanRequestRepository loanRequestRepository)
        {
            _loanRequestRepository = loanRequestRepository;
        }

        public async Task<IEnumerable<LoanRequest>> GetAllAsync() => await _loanRequestRepository.GetAllAsync();

        public async Task<LoanRequest?> GetByIdAsync(int loanRequestId) =>
            await _loanRequestRepository.GetByIdWithDetailsAsync(loanRequestId);

        public async Task<IEnumerable<LoanRequest>> GetByCustomerIdAsync(int customerId) =>
            await _loanRequestRepository.GetByCustomerIdAsync(customerId);

        public async Task<LoanRequest> ApplyAsync(LoanRequest loanRequest)
        {
            loanRequest.ApplyDate = DateTime.Now;
            loanRequest.Status = "Pending";
            await _loanRequestRepository.AddAsync(loanRequest);
            await _loanRequestRepository.SaveChangesAsync();
            return loanRequest;
        }

        public async Task<bool> UpdateStatusAsync(int loanRequestId, string status)
        {
            var loanRequest = await _loanRequestRepository.GetByIdAsync(loanRequestId);
            if (loanRequest == null) return false;

            loanRequest.Status = status;
            _loanRequestRepository.Update(loanRequest);
            return await _loanRequestRepository.SaveChangesAsync();
        }
    }
}
