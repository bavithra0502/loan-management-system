using LoanManagementAPI.Models;

namespace LoanManagementAPI.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(int customerId);
        Task<Customer?> GetByUserIdAsync(int userId);
        Task<bool> UpdateAsync(Customer customer);
    }
}
