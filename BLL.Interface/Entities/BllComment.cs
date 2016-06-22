using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    public class BllComment
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public BllUser Author { get; set; }
        public BllArticle Article { get; set; }
    }
}
