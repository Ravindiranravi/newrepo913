using Instiute01.Data;
using Instiute01.Dto;
using Instiute01.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Instiute01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class TeacherController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TeacherController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Teacher
        //[Authorize(Roles = "Admin,Teacher,")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherPostDto>>> GetTeachers()
        {
            var teachers = await _context.Teachers.ToListAsync();
            var teacherDTOs = teachers.Select(t => new TeacherPostDto
            {
                TeacherId = t.TeacherId,
                Username = t.Username,
                Password = t.Password,
                Address = t.Address,
                DateOfJoin = t.DateOfJoin,
                Email = t.Email,
                DateOfBirth = t.DateOfBirth,
                Qualification = t.Qualification
            }).ToList();

            return Ok(teacherDTOs);
        }

        // GET: api/Teacher/{id}
       // [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherPostDto>> GetTeacher(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }

            var teacherDTO = new TeacherPostDto
            {
                TeacherId = teacher.TeacherId,
                Username = teacher.Username,
                Password = teacher.Password,
                Address = teacher.Address,
                DateOfJoin = teacher.DateOfJoin,
                Email = teacher.Email,
                DateOfBirth = teacher.DateOfBirth,
                Qualification = teacher.Qualification
            };

            return Ok(teacherDTO);
        }

        // POST: api/Teacher
       // [Authorize(Roles = "Admin")]

        [HttpPost]
        public async Task<ActionResult<TeacherPostDto>> PostTeacher(TeacherPostDto teacherDTO)
        {
            var teacher = new Teacher
            {
                Username = teacherDTO.Username,
                Password = teacherDTO.Password,
                Address = teacherDTO.Address,
                DateOfJoin = teacherDTO.DateOfJoin,
                Email = teacherDTO.Email,
                DateOfBirth = teacherDTO.DateOfBirth,
                Qualification = teacherDTO.Qualification
            };

            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();

            teacherDTO.TeacherId = teacher.TeacherId;

            return CreatedAtAction(nameof(GetTeacher), new { id = teacher.TeacherId }, teacherDTO);
        }

        // PUT: api/Teacher/{id}
        //[Authorize(Roles = "Admin")]

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher(int id, TeacherPostDto teacherDTO)
        {
            if (id != teacherDTO.TeacherId)
            {
                return BadRequest();
            }

            var teacher = await _context.Teachers.FindAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }

            teacher.Username = teacherDTO.Username;
            teacher.Password = teacherDTO.Password;
            teacher.Address = teacherDTO.Address;
            teacher.DateOfJoin = teacherDTO.DateOfJoin;
            teacher.Email = teacherDTO.Email;
            teacher.DateOfBirth = teacherDTO.DateOfBirth;
            teacher.Qualification = teacherDTO.Qualification;

            _context.Entry(teacher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
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

        // DELETE: api/Teacher/{id}
        //[Authorize(Roles = "Admin")]

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeacherExists(int id)
        {
            return _context.Teachers.Any(e => e.TeacherId == id);
        }
    }
}
