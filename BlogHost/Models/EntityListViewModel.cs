using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogHost.Models
{
    public class EntityListViewModel<T>
    {
        public IEnumerable<T> Items { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}