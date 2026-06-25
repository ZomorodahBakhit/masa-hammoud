using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace university.Models
{
    public class Syllabus
    {
        [Key]
        public int Id { get; set; }
        public string? Description { get; set; }

    }
}