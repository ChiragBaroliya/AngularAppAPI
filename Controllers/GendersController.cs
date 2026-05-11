using Microsoft.AspNetCore.Mvc;
using AngularApp.BackendAPI.Data;
using AngularApp.BackendAPI.Models;

namespace AngularApp.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GendersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll() => Ok(MockDataContext.Genders);

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = MockDataContext.Genders.FirstOrDefault(x => x.Id == id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Gender gender)
        {
            gender.Id = MockDataContext.Genders.Any() ? MockDataContext.Genders.Max(x => x.Id) + 1 : 1;
            MockDataContext.Genders.Add(gender);
            return CreatedAtAction(nameof(GetById), new { id = gender.Id }, gender);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Gender gender)
        {
            var existing = MockDataContext.Genders.FirstOrDefault(x => x.Id == id);
            if (existing == null) return NotFound();
            existing.Name = gender.Name;
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = MockDataContext.Genders.FirstOrDefault(x => x.Id == id);
            if (existing == null) return NotFound();
            MockDataContext.Genders.Remove(existing);
            return NoContent();
        }
    }
}
