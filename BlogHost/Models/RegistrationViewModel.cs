using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogHost.Models
{
    public class RegistrationViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }

        //public string Capcha { get; set; }
    }
}