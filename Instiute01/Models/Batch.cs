namespace Instiute01.Models
{
    public class Batch
    {
        
            public int BatchId { get; set; }
            public string BatchName { get; set; }
            public DateTime StartDate { get; set; }
            public int StartYear { get; set; } // The start year of the batch
            public int EndYear { get; set; }   // The end year of the batch

            // Foreign key to the Course
            public int CourseId { get; set; }
            public Course Course { get; set; }

            // Navigation property for students
            public ICollection<Student> Students { get; set; }
        }

    }

