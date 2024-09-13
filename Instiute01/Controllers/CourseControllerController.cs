using Instiute01.Data;
using Instiute01.Dto;
using Instiute01.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Instiute01.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }
        //[Authorize(Roles = "Admin,Student,Teacher")]
        // GET: api/Course
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetCourses()
        {
            var courses = await _context.Courses
                .Select(c => new CourseDTO
                {
                    CourseId = c.CourseId,
                    CourseName = c.CourseName,
                    CourseDuration = c.CourseDuration,
                    CourseFees = c.CourseFees
                })
                .ToListAsync();

            return Ok(courses);
        }
        //[Authorize(Roles = "Admin,Teacher")]
        // GET: api/Course/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDTO>> GetCourse(int id)
        {
            var course = await _context.Courses
                .Where(c => c.CourseId == id)
                .Select(c => new CourseDTO
                {
                    CourseId = c.CourseId,
                    CourseName = c.CourseName,
                    CourseDuration = c.CourseDuration,
                    CourseFees = c.CourseFees
                })
                .FirstOrDefaultAsync();

            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }
       // [Authorize(Roles = "Admin")]
        // POST: api/Course
        [HttpPost]
        public async Task<ActionResult<CourseDTO>> PostCourse(CourseDTO courseDTO)
        {
            var course = new Course
            {
                CourseName = courseDTO.CourseName,
                CourseDuration = courseDTO.CourseDuration,
                CourseFees = courseDTO.CourseFees
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            courseDTO.CourseId = course.CourseId;

            return CreatedAtAction(nameof(GetCourse), new { id = course.CourseId }, courseDTO);
        }
       // [Authorize(Roles = "Admin,Teacher")]
        // PUT: api/Course/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, CourseDTO courseDTO)
        {
            if (id != courseDTO.CourseId)
            {
                return BadRequest();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            course.CourseName = courseDTO.CourseName;
            course.CourseDuration = courseDTO.CourseDuration;
            course.CourseFees = courseDTO.CourseFees;

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        //[Authorize(Roles = "Admin")]
        // DELETE: api/Course/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.CourseId == id);
        }
    }

}
