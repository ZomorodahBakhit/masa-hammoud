namespace University.Core.DTOs
    {
    public class CourseDto
        {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        }
    }