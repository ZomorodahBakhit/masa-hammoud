using System.ComponentModel.DataAnnotations;

namespace university.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Role { get; set; }

        public ICollection<Course> AuthoredCourses { get; set; } = new List<Course>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Grade> Grades { get; set; } = new List<Grade>();
    }
}