using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TMS_Project.Data;
using TMS_Project.Models;

namespace TMS_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskAttachmentsController : ControllerBase
    {
        private readonly TMSContext _context;

        public TaskAttachmentsController(TMSContext context)
        {
            _context = context;
        }

        [HttpGet("{taskId}")]
        [Authorize(Roles = "Admin,Manager,TeamMember")]
        public IActionResult GetTaskAttachments(int taskId)
        {
            var attachments = _context.TaskAttachments.Where(a => a.TaskId == taskId).ToList();
            return Ok(attachments);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager,TeamMember")]
        public IActionResult CreateTaskAttachment([FromBody] TaskAttachment attachment)
        {
            _context.TaskAttachments.Add(attachment);
            _context.SaveChanges();
            return Ok(attachment);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult DeleteTaskAttachment(int id)
        {
            var attachment = _context.TaskAttachments.Find(id);
            if (attachment == null)
                return NotFound();

            _context.TaskAttachments.Remove(attachment);
            _context.SaveChanges();
            return Ok();
        }
    }
}
