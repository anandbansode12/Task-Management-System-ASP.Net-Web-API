using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TMS_Project.Data;
using TMS_Project.Models;

namespace TMS_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskNotesController : ControllerBase
    {
        private readonly TMSContext _context;

        public TaskNotesController(TMSContext context)
        {
            _context = context;
        }

        [HttpGet("{taskId}")]
        [Authorize(Roles = "Admin,Manager,TeamMember")]
        public IActionResult GetTaskNotes(int taskId)
        {
            var notes = _context.TaskNotes.Where(n => n.TaskId == taskId).ToList();
            return Ok(notes);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager,TeamMember")]
        public IActionResult CreateTaskNote([FromBody] TaskNote note)
        {
            _context.TaskNotes.Add(note);
            _context.SaveChanges();
            return Ok(note);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager,TeamMember")]
        public IActionResult UpdateTaskNote(int id, [FromBody] TaskNote note)
        {
            var existingNote = _context.TaskNotes.Find(id);
            if (existingNote == null)
                return NotFound();

            existingNote.Note = note.Note;

            _context.TaskNotes.Update(existingNote);
            _context.SaveChanges();
            return Ok(existingNote);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult DeleteTaskNote(int id)
        {
            var note = _context.TaskNotes.Find(id);
            if (note == null)
                return NotFound();

            _context.TaskNotes.Remove(note);
            _context.SaveChanges();
            return Ok();
        }
    }
}
