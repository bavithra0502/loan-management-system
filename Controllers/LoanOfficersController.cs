using LoanManagementAPI.Models;
using LoanManagementAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoanManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LoanOfficersController : ControllerBase
    {
        private readonly ILoanOfficerService _officerService;

        public LoanOfficersController(ILoanOfficerService officerService)
        {
            _officerService = officerService;
        }

        // GET api/LoanOfficers
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll() => Ok(await _officerService.GetAllAsync());

        // GET api/LoanOfficers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var officer = await _officerService.GetByIdAsync(id);
            return officer == null ? NotFound() : Ok(officer);
        }

        // GET api/LoanOfficers/by-user/5
        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var officer = await _officerService.GetByUserIdAsync(userId);
            return officer == null ? NotFound() : Ok(officer);
        }

        // PUT api/LoanOfficers (self-service profile update)
        [HttpPut]
        public async Task<IActionResult> Update(LoanOfficer officer)
        {
            var success = await _officerService.UpdateAsync(officer);
            return success ? Ok(new { message = "Profile updated." }) : NotFound();
        }
    }
}
