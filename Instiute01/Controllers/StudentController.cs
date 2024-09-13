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
   
        public class StudentController : ControllerBase
        {
            private readonly ApplicationDbContext _context;

            public StudentController(ApplicationDbContext context)
            {
                _context = context;
            }
       // [Authorize(Roles = "Admin")]

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudents()
        {
            var students = await _context.Students
                .Select(s => new StudentDto
                {
                    StudentId = s.StudentId,
                    Username = s.Username,
                    Email = s.Email,
                    Contact = s.Contact,
                    Gender = s.Gender,
                    DateOfBirth = s.DateOfBirth,
                    Address = s.Address,
                    Qualification = s.Qualification,
                    InterestToStudy = s.InterestToStudy,
                    DateOfJoin = s.DateOfJoin
                }).ToListAsync();

            return Ok(students);
        }
       // [Authorize(Roles = "Admin,Student")]

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetStudent(int id)
        {
            var student = await _context.Students
                .Where(s => s.StudentId == id)
                .Select(s => new StudentDto
                {
                    StudentId = s.StudentId,
                    Username = s.Username,
                    Email = s.Email,
                    Contact = s.Contact,
                    Gender = s.Gender,
                    DateOfBirth = s.DateOfBirth,
                    Address = s.Address,
                    Qualification = s.Qualification,
                    InterestToStudy = s.InterestToStudy,
                    DateOfJoin = s.DateOfJoin
                }).FirstOrDefaultAsync();

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }
       // [Authorize(Roles = "Admin)]

        [HttpPost]
        public async Task<ActionResult<StudentDto>> PostStudent(StudentDto studentDTO)
        {
            var student = new Student
            {
                Username = studentDTO.Username,
                Password = studentDTO.Password, // Note: Handle passwords securely in practice
                Email = studentDTO.Email,
                Contact = studentDTO.Contact,
                Gender = studentDTO.Gender,
                DateOfBirth = studentDTO.DateOfBirth,
                Address = studentDTO.Address,
                Qualification = studentDTO.Qualification,
                InterestToStudy = studentDTO.InterestToStudy,
                DateOfJoin = studentDTO.DateOfJoin
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            studentDTO.StudentId = student.StudentId; // Set the generated ID

            return CreatedAtAction(nameof(GetStudent), new { id = student.StudentId }, studentDTO);
        }
       // [Authorize(Roles = "Admin,Student")]

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, StudentDto studentDTO)
        {
            if (id != studentDTO.StudentId)
            {
                return BadRequest();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            student.Username = studentDTO.Username;
            student.Password = studentDTO.Password; // Handle passwords securely in practice
            student.Email = studentDTO.Email;
            student.Contact = studentDTO.Contact;
            student.Gender = studentDTO.Gender;
            student.DateOfBirth = studentDTO.DateOfBirth;
            student.Address = studentDTO.Address;
            student.Qualification = studentDTO.Qualification;
            student.InterestToStudy = studentDTO.InterestToStudy;
            student.DateOfJoin = studentDTO.DateOfJoin;

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Students.Any(e => e.StudentId == id))
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

        // DELETE: api/Student/{id}
        //[Authorize(Roles = "Admin")]

        [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteStudent(int id)
            {
                var student = await _context.Students.FindAsync(id);
                if (student == null)
                {
                    return NotFound();
                }

                _context.Students.Remove(student);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            private bool StudentExists(int id)
            {
                return _context.Students.Any(e => e.StudentId == id);
            }
        }
    }

