using LoanManagementAPI.Models;

namespace LoanManagementAPI.Services
{
    public interface IFeedbackService
    {
        Task<IEnumerable<Feedback>> GetAllAsync();
        Task<IEnumerable<Feedback>> GetByCustomerIdAsync(int customerId);
        Task<Feedback> AddAsync(Feedback feedback);
    }
}
