using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TMS_Project.Data;
using TMS_Project.Models;

namespace TMS_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Manager,TeamMember")] 

    public class TasksController : ControllerBase
    {
        private readonly TMSContext _context;

        public TasksController(TMSContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Manager,TeamMember")]
        public IActionResult GetTasks()
        {
            var tasks = _context.Task1s.ToList();
            return Ok(tasks);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult CreateTask([FromBody] Task1 task)
        {
            _context.Task1s.Add(task);
            _context.SaveChanges();
            return Ok(task);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager,TeamMember")]
        public IActionResult UpdateTask(int id, [FromBody] Task1 task)
        {
            var existingTask = _context.Task1s.Find(id);
            if (existingTask == null)
                return NotFound();

            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.Status = task.Status;
            existingTask.DueDate = task.DueDate;

            _context.Task1s.Update(existingTask);
            _context.SaveChanges();
            return Ok(existingTask);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult DeleteTask(int id)
        {
            var task = _context.Task1s.Find(id);
            if (task == null)
                return NotFound();

            _context.Task1s.Remove(task);
            _context.SaveChanges();
            return Ok();
        }
    }
}


