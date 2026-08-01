using LoanManagementAPI.Models;
using LoanManagementAPI.Repositories;

namespace LoanManagementAPI.Services
{
    public class FeedbackQuestionService : IFeedbackQuestionService
    {
        private readonly IRepository<FeedbackQuestion> _repository;

        public FeedbackQuestionService(IRepository<FeedbackQuestion> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<FeedbackQuestion>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<IEnumerable<FeedbackQuestion>> GetActiveAsync() =>
            await _repository.FindAsync(q => q.IsActive);

        public async Task<FeedbackQuestion?> GetByIdAsync(int questionId) => await _repository.GetByIdAsync(questionId);

        public async Task<FeedbackQuestion> AddAsync(FeedbackQuestion question)
        {
            await _repository.AddAsync(question);
            await _repository.SaveChangesAsync();
            return question;
        }

        public async Task<bool> UpdateAsync(FeedbackQuestion question)
        {
            _repository.Update(question);
            return await _repository.SaveChangesAsync();
        }
    }
}
