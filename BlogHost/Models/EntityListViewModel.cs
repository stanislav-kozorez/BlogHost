using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogHost.Models
{
    public class EntityListViewModel
    {
        public IEnumerable<ArticleViewModel> Articles { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}