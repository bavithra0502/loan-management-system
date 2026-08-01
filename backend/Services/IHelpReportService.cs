using LoanManagementAPI.Models;

namespace LoanManagementAPI.Services
{
    public interface IHelpReportService
    {
        Task<IEnumerable<HelpReport>> GetAllAsync();
        Task<HelpReport?> GetByIdAsync(int helpReportId);
        Task<IEnumerable<HelpReport>> GetByUserIdAsync(int userId);
        Task<HelpReport> CreateAsync(HelpReport helpReport);
        Task<bool> ReplyAsync(int helpReportId, string reply, string status);
    }
}
