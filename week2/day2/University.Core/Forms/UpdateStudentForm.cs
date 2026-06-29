using System.ComponentModel.DataAnnotations;

namespace University.Core.Forms
    {
    public class UpdateStudentForm
        {

        [Required (ErrorMessage = "Name is required")]
        [MinLength (2, ErrorMessage = "Name must be at least 2 characters")]
        [MaxLength (100, ErrorMessage = "Name must not exceed 100 characters")]
        public string Name { get; set; } = null!;

        [Required (ErrorMessage = "Email is required")]
        [EmailAddress (ErrorMessage = "Email format is invalid")]
        [MaxLength (150, ErrorMessage = "Email must not exceed 150 characters")]
        public string Email { get; set; } = null!;
        }
    }