using LoanManagementAPI.Models;
using LoanManagementAPI.Repositories;

namespace LoanManagementAPI.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IRepository<Feedback> _repository;

        public FeedbackService(IRepository<Feedback> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Feedback>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<IEnumerable<Feedback>> GetByCustomerIdAsync(int customerId) =>
            await _repository.FindAsync(f => f.CustomerId == customerId);

        public async Task<Feedback> AddAsync(Feedback feedback)
        {
            feedback.FeedbackDate = DateTime.Now;
            await _repository.AddAsync(feedback);
            await _repository.SaveChangesAsync();
            return feedback;
        }
    }
}
