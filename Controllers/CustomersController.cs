using LoanManagementAPI.Models;
using LoanManagementAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoanManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET api/Customers
        [HttpGet]
        [Authorize(Roles = "Admin,LoanOfficer")]
        public async Task<IActionResult> GetAll() => Ok(await _customerService.GetAllAsync());

        // GET api/Customers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            return customer == null ? NotFound() : Ok(customer);
        }

        // GET api/Customers/by-user/5
        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var customer = await _customerService.GetByUserIdAsync(userId);
            return customer == null ? NotFound() : Ok(customer);
        }

        // PUT api/Customers  (self-service profile update)
        [HttpPut]
        public async Task<IActionResult> Update(Customer customer)
        {
            var success = await _customerService.UpdateAsync(customer);
            return success ? Ok(new { message = "Profile updated." }) : NotFound();
        }
    }
}
