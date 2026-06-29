using System.ComponentModel.DataAnnotations;

namespace University.Core.Attributes
    {
    public class FutureDateAttribute : ValidationAttribute
        {
        protected override ValidationResult? IsValid ( object? value, ValidationContext validationContext )
            {
            if ( value is DateTime date && date < DateTime.Today )
                return new ValidationResult (ErrorMessage ?? "Date must not be in the past");

            return ValidationResult.Success;
            }
        }
    }