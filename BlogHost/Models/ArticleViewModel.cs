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
        public string Title { get; set; }
        [AllowHtml]
        public string Text { get; set; }
        public string Tag1 { get; set; }
        public string Tag2 { get; set; }
        public string Tag3 { get; set; }
        public DateTime CreationDate { get; set; }
    }
}