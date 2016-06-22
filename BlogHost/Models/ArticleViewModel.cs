using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BlogHost.Models
{
    public class ArticleViewModel
    {
        public int ArticleId { get; set; }
        public int AuthorId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [AllowHtml]
        [Required(ErrorMessage = "Text is required")]
        public string Text { get; set; }
        [Required(ErrorMessage = "First tag is required")]
        public string Tag1 { get; set; }
        public string Tag2 { get; set; }
        public string Tag3 { get; set; }
        public DateTime CreationDate { get; set; }
    }
}