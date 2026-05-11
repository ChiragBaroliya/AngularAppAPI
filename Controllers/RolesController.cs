using Microsoft.AspNetCore.Mvc;
using AngularApp.BackendAPI.Data;
using AngularApp.BackendAPI.Models;

namespace AngularApp.BackendAPI.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll() => Ok(MockDataContext.Roles);

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = MockDataContext.Roles.FirstOrDefault(x => x.Id == id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Role role)
        {
            role.Id = MockDataContext.Roles.Any() ? MockDataContext.Roles.Max(x => x.Id) + 1 : 1;
            MockDataContext.Roles.Add(role);
            return CreatedAtAction(nameof(GetById), new { id = role.Id }, role);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Role role)
        {
                var existing = MockDataContext.Roles.FirstOrDefault(x => x.Id == id);
            if (existing == null) return NotFound();
            existing.Name = role.Name;
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = MockDataContext.Roles.FirstOrDefault(x => x.Id == id);
            if (existing == null) return NotFound();
            MockDataContext.Roles.Remove(existing);
            return NoContent();
        }
    }
}
