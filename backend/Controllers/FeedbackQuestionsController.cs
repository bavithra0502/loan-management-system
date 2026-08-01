using LoanManagementAPI.Models;
using LoanManagementAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoanManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FeedbackQuestionsController : ControllerBase
    {
        private readonly IFeedbackQuestionService _service;

        public FeedbackQuestionsController(IFeedbackQuestionService service)
        {
            _service = service;
        }

        // GET api/FeedbackQuestions  (admin - all, including inactive)
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        // GET api/FeedbackQuestions/active  (customer - active only)
        [HttpGet("active")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> GetActive() => Ok(await _service.GetActiveAsync());

        // GET api/FeedbackQuestions/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var question = await _service.GetByIdAsync(id);
            return question == null ? NotFound() : Ok(question);
        }

        // POST api/FeedbackQuestions
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(FeedbackQuestion question)
        {
            var result = await _service.AddAsync(question);
            return CreatedAtAction(nameof(GetById), new { id = result.QuestionId }, result);
        }

        // PUT api/FeedbackQuestions
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(FeedbackQuestion question)
        {
            var success = await _service.UpdateAsync(question);
            return success ? Ok(new { message = "Question updated." }) : NotFound();
        }
    }
}
