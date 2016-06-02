using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogHost.Models
{
    public class RegistrationViewModel: IValidatableObject
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(maximumLength:50, MinimumLength = 3, ErrorMessage = "Password must contain from 3 to 50 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "Password must contain from 3 to 50 characters")]
        public string RepeatPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (Password != RepeatPassword)
                errors.Add(new ValidationResult("Passwords do not match", new[] { "Password", "RepeatPassword" }));
            return errors;
        }
    }
}