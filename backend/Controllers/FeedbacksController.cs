using LoanManagementAPI.Models;
using LoanManagementAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoanManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackService _service;

        public FeedbacksController(IFeedbackService service)
        {
            _service = service;
        }

        // GET api/Feedbacks 
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        // GET api/Feedbacks/by-customer/5
        [HttpGet("by-customer/{customerId}")]
        public async Task<IActionResult> GetByCustomer(int customerId) =>
            Ok(await _service.GetByCustomerIdAsync(customerId));

        // POST api/Feedbacks 
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Add(Feedback feedback)
        {
            var result = await _service.AddAsync(feedback);
            return CreatedAtAction(nameof(GetByCustomer), new { customerId = result.CustomerId }, result);
        }
    }
}
