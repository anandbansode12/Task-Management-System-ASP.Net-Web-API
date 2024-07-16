using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMS_Project.Data;
using TMS_Project.Models;

namespace TMS_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RolesController : ControllerBase
    {
        private readonly TMSContext _context;

        public RolesController(TMSContext context)
        {
            _context = context;
        }

        // GET: api/roles
        [HttpGet]
        public IActionResult GetRoles()
        {
            var roles = _context.Roles.ToList();
            return Ok(roles);
        }

        // GET: api/roles/5
        [HttpGet("{id}")]
        public IActionResult GetRole(int id)
        {
            var role = _context.Roles.Find(id);
            if (role == null)
                return NotFound();

            return Ok(role);
        }

        // POST: api/roles
        [HttpPost]
        public IActionResult CreateRole([FromBody] Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetRole), new { id = role.RoleId }, role);
        }

        // PUT: api/roles/5
        [HttpPut("{id}")]
        public IActionResult UpdateRole(int id, [FromBody] Role role)
        {
            if (id != role.RoleId)
                return BadRequest();

            _context.Entry(role).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/roles/5
        [HttpDelete("{id}")]
        public IActionResult DeleteRole(int id)
        {
            var role = _context.Roles.Find(id);
            if (role == null)
                return NotFound();

            _context.Roles.Remove(role);
            _context.SaveChanges();
            return Ok();
        }
    }
}
