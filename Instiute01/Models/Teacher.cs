namespace Instiute01.Models
{
    public class Teacher
    {
        
            public int TeacherId { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string Address { get; set; }
            public DateTime DateOfJoin { get; set; }
            public string Email { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string Qualification { get; set; }

            // Navigation property: a teacher can teach multiple courses
            public ICollection<Course> Courses { get; set; }
        }

    }
