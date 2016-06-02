using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogHost.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "Password must contain from 3 to 50 characters")]
        public string Password { get; set; }
    }
}