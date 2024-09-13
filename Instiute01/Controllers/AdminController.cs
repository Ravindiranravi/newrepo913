using Instiute01.Data;
using Instiute01.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Instiute01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Admin/Statistics
        [HttpGet("Statistics")]
        public async Task<ActionResult<AdminStatisticsDTO>> GetStatistics()
        {
            var studentCount = await _context.Students.CountAsync();
            var traineeCount = await _context.Teachers.CountAsync();
            var courseCount = await _context.Courses.CountAsync();
            var batchCount = await _context.Batches.CountAsync();

            var statistics = new AdminStatisticsDTO
            {
                NumberOfStudents = studentCount,
                NumberOfTrainees = traineeCount,
                NumberOfCourses = courseCount,
                NumberOfBatches = batchCount
            };

            return Ok(statistics);
        }

    }
}
