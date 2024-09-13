using Instiute01.Data;
using Instiute01.Dto;
using Instiute01.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Instiute01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtService _jwtservice;
        public LoginController(ApplicationDbContext context,JwtService jwtService)
        {
            _context = context;
            _jwtservice = jwtService;
        }

        // POST: api/Login
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDTO)
        {
            if (loginDTO == null || string.IsNullOrEmpty(loginDTO.Role))
            {
                return BadRequest("Invalid login request.");
            }

            if (loginDTO.Role.Equals("Teacher", StringComparison.OrdinalIgnoreCase))
            {
                var teacher = await _context.Teachers
                    .FirstOrDefaultAsync(t => t.Username == loginDTO.Username);

                if (teacher == null || teacher.Password != loginDTO.Password)
                {
                    return Unauthorized("Invalid username or password.");
                }
                string token = _jwtservice.GenerateJwtToken(loginDTO);

                return Ok(new
                {
                    Id = teacher.TeacherId,
                    Role = "Teacher",
                    Token = token
                });
            }
            else if (loginDTO.Role.Equals("Student", StringComparison.OrdinalIgnoreCase))
            {
                var student = await _context.Students
                    .FirstOrDefaultAsync(s => s.Username == loginDTO.Username);

                if (student == null || student.Password != loginDTO.Password)
                {
                    return Unauthorized("Invalid username or password.");
                }
                string token = _jwtservice.GenerateJwtToken(loginDTO);

                // Successful login for Student
                return Ok(new
                {
                 Id = student.StudentId,
                    Role = "Student",
                    Token = token
                });
            }
            else if (loginDTO.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
              
if(loginDTO.Username=="admin" && loginDTO.Password=="admin@123")
                {
                    string token = _jwtservice.GenerateJwtToken(loginDTO);

                    return Ok(new
                    {
                    
                        Role = "admin",
                        Token =token
                    });
                }
                // Successful login for Student
                return BadRequest("Invalid role.");

            }
            else
            {
                return BadRequest("Invalid role.");
            }
        }
    }
}
