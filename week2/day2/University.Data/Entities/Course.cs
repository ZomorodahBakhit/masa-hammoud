namespace University.Data.Entities
    {
    public class Course
        {
        public int Id { get; set; }
        public required string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        }
    }