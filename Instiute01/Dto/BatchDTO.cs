namespace Instiute01.Dto
{
    public class BatchDTO
    {
        public int BatchId { get; set; }
        public string BatchName { get; set; }
        public DateTime StartDate { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public int CourseId { get; set; }
    }
}
