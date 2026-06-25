using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace university.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int TeacherId { get; set; }

        [ForeignKey("TeacherId")]
        public User Teacher { get; set; }

        public int? SyllabusId { get; set; }

        [ForeignKey("SyllabusId")]
        public Syllabus? Syllabus { get; set; }

        public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
    }
}