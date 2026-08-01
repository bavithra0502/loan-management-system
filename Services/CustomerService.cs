using LoanManagementAPI.Models;
using LoanManagementAPI.Repositories;

namespace LoanManagementAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;

        public CustomerService(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync() => await _customerRepository.GetAllAsync();

        public async Task<Customer?> GetByIdAsync(int customerId) => await _customerRepository.GetByIdAsync(customerId);

        public async Task<Customer?> GetByUserIdAsync(int userId)
        {
            var results = await _customerRepository.FindAsync(c => c.UserId == userId);
            return results.FirstOrDefault();
        }

        public async Task<bool> UpdateAsync(Customer customer)
        {
            _customerRepository.Update(customer);
            return await _customerRepository.SaveChangesAsync();
        }
    }
}
