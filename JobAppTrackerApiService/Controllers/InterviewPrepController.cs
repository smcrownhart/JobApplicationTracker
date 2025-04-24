using JobApplicationTracker.DataAccess.Interfaces;
using JobApplicationTracker.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobAppTrackerApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InterviewPrepController : ControllerBase
    {
        
        private readonly IJobAppTracker<InterviewPrep> _repository;

        public InterviewPrepController(IJobAppTracker<InterviewPrep> repository)
        {
            _repository = repository;
        }

        [HttpGet]

        public async Task<IEnumerable<InterviewPrep>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        [HttpGet("id")]

        public async Task<ActionResult<InterviewPrep>> GetById(int id)
        {
            var app = await _repository.GetByIdAsync(id);
            if (app == null) return NotFound();
            return Ok(app);
        }

        [HttpPost]

        public async Task<ActionResult<InterviewPrep>> Add(InterviewPrep app)
        {
            var create = await _repository.AddAsync(app);
            return CreatedAtAction(nameof(GetById), new { id = create.Id }, create);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, InterviewPrep app)
        {
            if (id != app.Id) return BadRequest("ID not found");
            await _repository.UpdateAsync(app);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}

