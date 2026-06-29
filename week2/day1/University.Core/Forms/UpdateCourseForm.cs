using System.ComponentModel.DataAnnotations;
using University.Core.Attributes;

namespace University.Core.Forms
    {
    public class UpdateCourseForm
        {
        [Required (ErrorMessage = "Title is required")]
        [MinLength (2, ErrorMessage = "Title must be at least 2 characters")]
        [MaxLength (100, ErrorMessage = "Title must not exceed 100 characters")]
        public string Title { get; set; } = null!;

        [Required (ErrorMessage = "Start date is required")]
        [FutureDate (ErrorMessage = "Start date must not be in the past")]
        public DateTime StartDate { get; set; }

        [Required (ErrorMessage = "End date is required")]
        [FutureDate (ErrorMessage = "End date must not be in the past")]
        public DateTime EndDate { get; set; }

        public IEnumerable<ValidationResult> Validate ( ValidationContext validationContext )
            {
            if ( EndDate <= StartDate )
                yield return new ValidationResult (
                    "End date must be after Start date",
                    new [] { nameof (EndDate) });
            }
        }
    }