using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BlogHost.Models
{
    public class UserEditViewModel
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "User name is required")]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [Range(1, 3, ErrorMessage = "Chosen role is incorrect")]
        public int RoleId { get; set; } 
        public SelectList Roles { get; set; }
    }
}