using Microsoft.AspNetCore.Mvc;
using AngularApp.BackendAPI.Data;
using AngularApp.BackendAPI.Models;

namespace AngularApp.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll() => Ok(MockDataContext.Designations);

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = MockDataContext.Designations.FirstOrDefault(x => x.Id == id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Designation designation)
        {
            designation.Id = MockDataContext.Designations.Any() ? MockDataContext.Designations.Max(x => x.Id) + 1 : 1;
            MockDataContext.Designations.Add(designation);
            return CreatedAtAction(nameof(GetById), new { id = designation.Id }, designation);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Designation designation)
        {
            var existing = MockDataContext.Designations.FirstOrDefault(x => x.Id == id);
            if (existing == null) return NotFound();
            existing.Name = designation.Name;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = MockDataContext.Designations.FirstOrDefault(x => x.Id == id);
            if (existing == null) return NotFound();
            MockDataContext.Designations.Remove(existing);
            return NoContent();
        }
    }
}
