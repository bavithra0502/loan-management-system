using LoanManagementAPI.DTOs;
using LoanManagementAPI.Models;

namespace LoanManagementAPI.Services
{
    public interface IUserService
    {
        Task<LoginResponseDto?> LoginAsync(string userName, string password);
        Task<User> RegisterCustomerAsync(RegisterCustomerDto dto);
        Task<User> RegisterOfficerAsync(RegisterOfficerDto dto);
        Task<IEnumerable<User>> GetAllAsync();
        Task<IEnumerable<User>> GetByRoleAsync(string role);
        Task<User?> GetByIdAsync(int userId);
        Task<bool> UpdateStatusAsync(int userId, string status);
        Task<bool> ChangePasswordAsync(ChangePasswordDto dto);
        Task<bool> DeleteAsync(int userId);
    }
}
