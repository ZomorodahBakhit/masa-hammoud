using System.ComponentModel.DataAnnotations;

namespace University.Core.Forms
    {
    public class RegisterForm
        {
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength (6)]
        public string Password { get; set; } = null!;

        [Required]
        public string Role { get; set; } = null!;
        }
    }