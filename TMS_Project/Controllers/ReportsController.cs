using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TMS_Project.Data;

namespace TaskManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly TMSContext _context;

        public ReportsController(TMSContext context)
        {
            _context = context;
        }

        [HttpGet("weekly")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetWeeklyReport()
        {
            var now = DateTime.UtcNow;
            var oneWeekAgo = now.AddDays(-7);

            var tasks = _context.Task1s.Where(t => t.CreatedDate >= oneWeekAgo && t.CreatedDate <= now).ToList();
            return Ok(tasks);
        }

        [HttpGet("monthly")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetMonthlyReport()
        {
            var now = DateTime.UtcNow;
            var oneMonthAgo = now.AddMonths(-1);

            var tasks = _context.Task1s.Where(t => t.CreatedDate >= oneMonthAgo && t.CreatedDate <= now).ToList();
            return Ok(tasks);
        }
    }
}
