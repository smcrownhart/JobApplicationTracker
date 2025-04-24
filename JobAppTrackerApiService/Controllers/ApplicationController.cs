using Microsoft.AspNetCore.Mvc;
using JobApplicationTracker.DataAccess.Models;
using JobApplicationTracker.DataAccess.Interfaces;

namespace JobAppTrackerApiService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationController: ControllerBase
    {
        private readonly IJobAppTracker<Application> _repository;

        public ApplicationController(IJobAppTracker<Application> repository)
        {
            _repository = repository;
        }

        [HttpGet("id")]

        public async Task<ActionResult<Application>> GetById(int id)
        {
            var app = await _repository.GetByIdAsync(id);
            if (app == null) return NotFound();
            return Ok(app);
        }

        [HttpPost]

        public async Task<ActionResult<Application>> Add(Application app)
        {
            var create = await _repository.AddAsync(app);
            return CreatedAtAction(nameof(GetById), new {id = create.Id}, create);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update (int id, Application app)
        {
            if (id != app.Id) return BadRequest("ID not found");
            await _repository.UpdateAsync(app);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task <IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
