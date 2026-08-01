using LoanManagementAPI.Models;
using LoanManagementAPI.Repositories;

namespace LoanManagementAPI.Services
{
    public class LoanOfficerService : ILoanOfficerService
    {
        private readonly IRepository<LoanOfficer> _officerRepository;

        public LoanOfficerService(IRepository<LoanOfficer> officerRepository)
        {
            _officerRepository = officerRepository;
        }

        public async Task<IEnumerable<LoanOfficer>> GetAllAsync() => await _officerRepository.GetAllAsync();

        public async Task<LoanOfficer?> GetByIdAsync(int officerId) => await _officerRepository.GetByIdAsync(officerId);

        public async Task<LoanOfficer?> GetByUserIdAsync(int userId)
        {
            var results = await _officerRepository.FindAsync(o => o.UserId == userId);
            return results.FirstOrDefault();
        }

        public async Task<bool> UpdateAsync(LoanOfficer officer)
        {
            _officerRepository.Update(officer);
            return await _officerRepository.SaveChangesAsync();
        }
    }
}
