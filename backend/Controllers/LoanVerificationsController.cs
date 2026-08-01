using LoanManagementAPI.DTOs;
using LoanManagementAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoanManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LoanVerificationsController : ControllerBase
    {
        private readonly ILoanVerificationService _service;

        public LoanVerificationsController(ILoanVerificationService service)
        {
            _service = service;
        }

        // GET api/LoanVerifications
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        // GET api/LoanVerifications/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var verification = await _service.GetByIdAsync(id);
            return verification == null ? NotFound() : Ok(verification);
        }

        // GET api/LoanVerifications/by-officer/5
        [HttpGet("by-officer/{officerId}")]
        [Authorize(Roles = "LoanOfficer,Admin")]
        public async Task<IActionResult> GetByOfficer(int officerId) =>
            Ok(await _service.GetByOfficerIdAsync(officerId));

        // POST api/LoanVerifications/assign  (admin assigns an officer)
        [HttpPost("assign")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Assign(AssignOfficerDto dto)
        {
            var result = await _service.AssignAsync(dto.LoanRequestId, dto.OfficerId);
            return CreatedAtAction(nameof(GetById), new { id = result.LoanVerificationId }, result);
        }

        // PUT api/LoanVerifications/5  (officer finalizes: Approved/Rejected)
        // Also cascades to update the parent LoanRequest.Status.
        public class LoanVerificationUpdateDto
        {
            public string VerificationResult { get; set; } = string.Empty;
            public string Status { get; set; } = string.Empty;
            public string? Remarks { get; set; }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "LoanOfficer,Admin")]
        public async Task<IActionResult> Update(int id, LoanVerificationUpdateDto dto)
        {
            var success = await _service.UpdateAsync(id, dto.VerificationResult, dto.Status, dto.Remarks ?? string.Empty);
            return success ? Ok(new { message = "Loan verification updated." }) : NotFound();
        }

        // DELETE api/LoanVerifications/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? Ok(new { message = "Loan verification deleted." }) : NotFound();
        }
    }
}
