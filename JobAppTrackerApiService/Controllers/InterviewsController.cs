using JobApplicationTracker.DataAccess.Interfaces;
using JobApplicationTracker.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobAppTrackerApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InterviewsController: ControllerBase
    {
        private readonly IJobAppTracker<Interviews> _repository;

        public InterviewsController(IJobAppTracker<Interviews> repository)
        {
            _repository = repository;
        }

        [HttpGet]

        public async Task<IEnumerable<Interviews>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        [HttpGet("id")]

        public async Task<ActionResult<Interviews>> GetById(int id)
        {
            var app = await _repository.GetByIdAsync(id);
            if (app == null) return NotFound();
            return Ok(app);
        }

        [HttpPost]

        public async Task<ActionResult<Interviews>> Add(Interviews app)
        {
            var create = await _repository.AddAsync(app);
            return CreatedAtAction(nameof(GetById), new { id = create.Id }, create);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, Interviews app)
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

