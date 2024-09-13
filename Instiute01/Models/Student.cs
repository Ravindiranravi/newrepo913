namespace Instiute01.Models
{
    public class Student
    {
        public int StudentId { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public string Contact { get; set; }
            public string Gender { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string Address { get; set; }
            public string Qualification { get; set; }
            public string InterestToStudy { get; set; }
            public DateTime DateOfJoin { get; set; } // The date the student joined

            // Navigation property for batches
            public ICollection<Batch> Batches { get; set; }
        }

    }

