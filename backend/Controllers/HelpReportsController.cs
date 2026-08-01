using LoanManagementAPI.Models;
using LoanManagementAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoanManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class HelpReportsController : ControllerBase
    {
        private readonly IHelpReportService _service;

        public HelpReportsController(IHelpReportService service)
        {
            _service = service;
        }

        // GET api/HelpReports
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        // GET api/HelpReports/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var report = await _service.GetByIdAsync(id);
            return report == null ? NotFound() : Ok(report);
        }

        // GET api/HelpReports/by-user/5
        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId) => Ok(await _service.GetByUserIdAsync(userId));

        // POST api/HelpReports (customer or officer raises a ticket)
        [HttpPost]
        [Authorize(Roles = "Customer,LoanOfficer")]
        public async Task<IActionResult> Create(HelpReport helpReport)
        {
            var result = await _service.CreateAsync(helpReport);
            return CreatedAtAction(nameof(GetById), new { id = result.HelpReportId }, result);
        }

        public class ReplyDto
        {
            public string Reply { get; set; } = string.Empty;
            public string Status { get; set; } = "Closed";
        }

        // PUT api/HelpReports/5/reply  (admin replies and closes it)
        [HttpPut("{id}/reply")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Reply(int id, ReplyDto dto)
        {
            var success = await _service.ReplyAsync(id, dto.Reply, dto.Status);
            return success ? Ok(new { message = "Reply submitted." }) : NotFound();
        }
    }
}
