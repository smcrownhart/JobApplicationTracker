using JobApplicationTracker.DataAccess.Interfaces;
using JobApplicationTracker.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobAppTrackerApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyContactController: ControllerBase
    {
        
        private readonly IJobAppTracker<CompanyContact> _repository;

        public CompanyContactController(IJobAppTracker<CompanyContact> repository)
        {
            _repository = repository;
        }

        [HttpGet]

        public async Task<IEnumerable<CompanyContact>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        [HttpGet("id")]

        public async Task<ActionResult<CompanyContact>> GetById(int id)
        {
            var app = await _repository.GetByIdAsync(id);
            if (app == null) return NotFound();
            return Ok(app);
        }

        [HttpPost]

        public async Task<ActionResult<CompanyContact>> Add(CompanyContact app)
        {
            var create = await _repository.AddAsync(app);
            return CreatedAtAction(nameof(GetById), new { id = create.Id }, create);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, CompanyContact app)
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

