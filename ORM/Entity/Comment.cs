using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Entity
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int Text { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual User Author { get; set; }
        public virtual Article Article { get; set; }
    }
}
