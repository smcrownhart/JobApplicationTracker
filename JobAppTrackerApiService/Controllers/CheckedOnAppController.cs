using Microsoft.AspNetCore.Mvc;
using JobApplicationTracker.DataAccess.Models;
using JobApplicationTracker.DataAccess.Interfaces;


namespace JobAppTrackerApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckedOnAppController: ControllerBase
    {
        
        
            private readonly IJobAppTracker<CheckedOnApp> _repository;

            public CheckedOnAppController(IJobAppTracker<CheckedOnApp> repository)
            {
                _repository = repository;
            }

            [HttpGet]

            public async Task<IEnumerable<CheckedOnApp>> GetAll()
            {
                return await _repository.GetAllAsync();
            }
            
            [HttpGet("id")]

            public async Task<ActionResult<CheckedOnApp>> GetById(int id)
            {
                var app = await _repository.GetByIdAsync(id);
                if (app == null) return NotFound();
                return Ok(app);
            }

            [HttpPost]

            public async Task<ActionResult<CheckedOnApp>> Add(CheckedOnApp app)
            {
                var create = await _repository.AddAsync(app);
                return CreatedAtAction(nameof(GetById), new { id = create.Id }, create);
            }

            [HttpPut("{id}")]

            public async Task<IActionResult> Update(int id, CheckedOnApp app)
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


