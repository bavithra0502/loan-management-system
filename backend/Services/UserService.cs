using LoanManagementAPI.DTOs;
using LoanManagementAPI.Helpers;
using LoanManagementAPI.Models;
using LoanManagementAPI.Repositories;

namespace LoanManagementAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<LoanOfficer> _officerRepository;
        private readonly JwtTokenHelper _jwtTokenHelper;

        public UserService(
            IUserRepository userRepository,
            IRepository<Customer> customerRepository,
            IRepository<LoanOfficer> officerRepository,
            JwtTokenHelper jwtTokenHelper)
        {
            _userRepository = userRepository;
            _customerRepository = customerRepository;
            _officerRepository = officerRepository;
            _jwtTokenHelper = jwtTokenHelper;
        }

        // Admin is auto-approved; Customer/LoanOfficer must wait for Admin approval
        private static int RoleToRoleId(string role) => role switch
        {
            "Admin" => 1,
            "LoanOfficer" => 2,
            "Customer" => 3,
            _ => 0
        };

        public async Task<LoginResponseDto?> LoginAsync(string userName, string password)
        {
            var user = await _userRepository.GetByUserNameAndPasswordAsync(userName, password);
            if (user == null) return null;

            if (user.Status != "Approved")
            {
                // Still return identity info so the front end can show a
                // "pending approval" / "rejected" message, but no usable token.
                return new LoginResponseDto
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    UName = user.UserName,
                    Role = user.Role,
                    RoleId = RoleToRoleId(user.Role),
                    Status = user.Status,
                    Token = string.Empty
                };
            }

            return new LoginResponseDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                UName = user.UserName,
                Role = user.Role,
                RoleId = RoleToRoleId(user.Role),
                Status = user.Status,
                Token = _jwtTokenHelper.GenerateToken(user)
            };
        }

        public async Task<User> RegisterCustomerAsync(RegisterCustomerDto dto)
        {
            var existing = await _userRepository.GetByUserNameAsync(dto.UserName);
            if (existing != null) throw new InvalidOperationException("Username already exists.");

            var user = new User
            {
                UserName = dto.UserName,
                Password = dto.Password,
                Role = "Customer",
                Status = "Pending",
                CreatedDate = DateTime.Now
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            var customer = new Customer
            {
                UserId = user.UserId,
                CustomerName = dto.CustomerName,
                Gender = dto.Gender,
                DOB = dto.DOB,
                Phone = dto.Phone,
                Email = dto.Email,
                Address = dto.Address,
                AadhaarNumber = dto.AadhaarNumber,
                PANNumber = dto.PANNumber,
                Occupation = dto.Occupation,
                AnnualIncome = dto.AnnualIncome
            };

            await _customerRepository.AddAsync(customer);
            await _customerRepository.SaveChangesAsync();

            return user;
        }

        public async Task<User> RegisterOfficerAsync(RegisterOfficerDto dto)
        {
            var existing = await _userRepository.GetByUserNameAsync(dto.UserName);
            if (existing != null) throw new InvalidOperationException("Username already exists.");

            var user = new User
            {
                UserName = dto.UserName,
                Password = dto.Password,
                Role = "LoanOfficer",
                Status = "Pending",
                CreatedDate = DateTime.Now
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            var officer = new LoanOfficer
            {
                UserId = user.UserId,
                OfficerName = dto.OfficerName,
                Phone = dto.Phone,
                Email = dto.Email,
                Address = dto.Address,
                EmployeeCode = dto.EmployeeCode
            };

            await _officerRepository.AddAsync(officer);
            await _officerRepository.SaveChangesAsync();

            return user;
        }

        public async Task<IEnumerable<User>> GetAllAsync() => await _userRepository.GetAllAsync();

        public async Task<IEnumerable<User>> GetByRoleAsync(string role) =>
            await _userRepository.FindAsync(u => u.Role == role);

        public async Task<User?> GetByIdAsync(int userId) => await _userRepository.GetByIdAsync(userId);

        public async Task<bool> UpdateStatusAsync(int userId, string status)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return false;

            user.Status = status;
            _userRepository.Update(user);
            return await _userRepository.SaveChangesAsync();
        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordDto dto)
        {
            var user = await _userRepository.GetByIdAsync(dto.UserId);
            if (user == null || user.Password != dto.OldPassword) return false;

            user.Password = dto.NewPassword;
            _userRepository.Update(user);
            return await _userRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return false;

            _userRepository.Delete(user);
            return await _userRepository.SaveChangesAsync();
        }
    }
}
