namespace Instiute01.Dto
{
    public class CourseDTO
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int CourseDuration { get; set; } // Duration in weeks or months
        public decimal CourseFees { get; set; }
    }
}
