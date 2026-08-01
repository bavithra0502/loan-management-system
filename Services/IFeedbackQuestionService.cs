using LoanManagementAPI.Models;

namespace LoanManagementAPI.Services
{
    public interface IFeedbackQuestionService
    {
        Task<IEnumerable<FeedbackQuestion>> GetAllAsync();
        Task<IEnumerable<FeedbackQuestion>> GetActiveAsync();
        Task<FeedbackQuestion?> GetByIdAsync(int questionId);
        Task<FeedbackQuestion> AddAsync(FeedbackQuestion question);
        Task<bool> UpdateAsync(FeedbackQuestion question);
    }
}
