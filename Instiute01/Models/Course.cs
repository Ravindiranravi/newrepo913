namespace Instiute01.Models
{
    public class Course
    {
       
            public int CourseId { get; set; }
            public string CourseName { get; set; }
            public int CourseDuration { get; set; } // Duration in weeks or months
            public decimal CourseFees { get; set; }

            // Navigation properties
            public ICollection<Teacher> Teachers { get; set; }
            public ICollection<Batch> Batches { get; set; }
        }

    }

