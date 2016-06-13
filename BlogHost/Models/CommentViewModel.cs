using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogHost.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public int Text { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public int ArticleId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}