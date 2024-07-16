using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TMS_Project.Data;
using TMS_Project.Models;

namespace TMS_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly TMSContext _context;

        public UsersController(TMSContext context)
        {
            _context = context;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin,Manager")]
        public IActionResult GetUsers()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin,Manager,TeamMember")]
        public IActionResult GetUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public IActionResult CreateUser([FromBody] User user)
        {
            var role = _context.Roles.SingleOrDefault(r => r.RoleId == user.RoleId);
            if (role == null)
            {
                return BadRequest("Invalid role.");
            }

            user.Role = role; 

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(user);
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin,Manager")]
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            var existingUser = _context.Users.Find(id);
            if (existingUser == null)
                return NotFound();

            existingUser.Username = user.Username;
            existingUser.Email = user.Email;
            existingUser.RoleId = user.RoleId;

            if (!string.IsNullOrWhiteSpace(user.PasswordHash))
            {
                existingUser.PasswordHash = user.PasswordHash;
            }

            _context.Users.Update(existingUser);
            _context.SaveChanges();
            return Ok(existingUser);
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            _context.SaveChanges();
            return Ok();
        }
    }
}
