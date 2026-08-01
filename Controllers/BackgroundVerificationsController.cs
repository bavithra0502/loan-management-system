using LoanManagementAPI.DTOs;
using LoanManagementAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoanManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BackgroundVerificationsController : ControllerBase
    {
        private readonly IBackgroundVerificationService _service;

        public BackgroundVerificationsController(IBackgroundVerificationService service)
        {
            _service = service;
        }

        // GET api/BackgroundVerifications
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        // GET api/BackgroundVerifications/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var verification = await _service.GetByIdAsync(id);
            return verification == null ? NotFound() : Ok(verification);
        }

        // GET api/BackgroundVerifications/by-officer/5
        [HttpGet("by-officer/{officerId}")]
        [Authorize(Roles = "LoanOfficer,Admin")]
        public async Task<IActionResult> GetByOfficer(int officerId) =>
            Ok(await _service.GetByOfficerIdAsync(officerId));

        // POST api/BackgroundVerifications/assign  (admin assigns an officer)
        [HttpPost("assign")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Assign(AssignOfficerDto dto)
        {
            var result = await _service.AssignAsync(dto.LoanRequestId, dto.OfficerId);
            return CreatedAtAction(nameof(GetById), new { id = result.VerificationId }, result);
        }

        // PUT api/BackgroundVerifications/5  (officer updates verification result)
        [HttpPut("{id}")]
        [Authorize(Roles = "LoanOfficer,Admin")]
        public async Task<IActionResult> Update(int id, StatusUpdateDto dto)
        {
            var success = await _service.UpdateAsync(id, dto.Status, dto.Remarks ?? string.Empty);
            return success ? Ok(new { message = "Background verification updated." }) : NotFound();
        }

        // DELETE api/BackgroundVerifications/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? Ok(new { message = "Background verification deleted." }) : NotFound();
        }
    }
}
