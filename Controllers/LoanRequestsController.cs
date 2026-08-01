using LoanManagementAPI.DTOs;
using LoanManagementAPI.Models;
using LoanManagementAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoanManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LoanRequestsController : ControllerBase
    {
        private readonly ILoanRequestService _loanRequestService;

        public LoanRequestsController(ILoanRequestService loanRequestService)
        {
            _loanRequestService = loanRequestService;
        }

        // GET api/LoanRequests
        [HttpGet]
        [Authorize(Roles = "Admin,LoanOfficer")]
        public async Task<IActionResult> GetAll() => Ok(await _loanRequestService.GetAllAsync());

        // GET api/LoanRequests/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var loanRequest = await _loanRequestService.GetByIdAsync(id);
            return loanRequest == null ? NotFound() : Ok(loanRequest);
        }

        // GET api/LoanRequests/by-customer/5
        [HttpGet("by-customer/{customerId}")]
        [Authorize(Roles = "Customer,Admin,LoanOfficer")]
        public async Task<IActionResult> GetByCustomer(int customerId) =>
            Ok(await _loanRequestService.GetByCustomerIdAsync(customerId));

        // POST api/LoanRequests  (customer applies for a loan)
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Apply(LoanRequest loanRequest)
        {
            var result = await _loanRequestService.ApplyAsync(loanRequest);
            return CreatedAtAction(nameof(GetById), new { id = result.LoanRequestId }, result);
        }

        // PUT api/LoanRequests/5/status  (admin approves/rejects overall request)
        [HttpPut("{id}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] StatusUpdateDto dto)
        {
            var success = await _loanRequestService.UpdateStatusAsync(id, dto.Status);
            return success ? Ok(new { message = $"Loan request {dto.Status}." }) : NotFound();
        }
    }
}
