using LoanManagementAPI.Models;
using LoanManagementAPI.Repositories;

namespace LoanManagementAPI.Services
{
    public class HelpReportService : IHelpReportService
    {
        private readonly IRepository<HelpReport> _repository;

        public HelpReportService(IRepository<HelpReport> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<HelpReport>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<HelpReport?> GetByIdAsync(int helpReportId) => await _repository.GetByIdAsync(helpReportId);

        public async Task<IEnumerable<HelpReport>> GetByUserIdAsync(int userId) =>
            await _repository.FindAsync(h => h.UserId == userId);

        public async Task<HelpReport> CreateAsync(HelpReport helpReport)
        {
            helpReport.Status = "Open";
            await _repository.AddAsync(helpReport);
            await _repository.SaveChangesAsync();
            return helpReport;
        }

        public async Task<bool> ReplyAsync(int helpReportId, string reply, string status)
        {
            var helpReport = await _repository.GetByIdAsync(helpReportId);
            if (helpReport == null) return false;

            helpReport.Reply = reply;
            helpReport.Status = status;
            _repository.Update(helpReport);
            return await _repository.SaveChangesAsync();
        }
    }
}
