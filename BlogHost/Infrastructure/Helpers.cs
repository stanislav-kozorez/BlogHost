using BlogHost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BlogHost.Infrastructure
{
    public static class Helpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
           
            if(pagingInfo.CurrentPage - 3 > 4)
            {
                for(int i = 1; i <= 2; i++)
                    result.Append(CreateLiTag(pagingInfo, i, pageUrl));

                result.Append(CreateLinkSeparator());

                for (int i = pagingInfo.CurrentPage - 3; i <= pagingInfo.CurrentPage; i++)
                    result.Append(CreateLiTag(pagingInfo, i, pageUrl));
            }
            else
                for(int i = 1; i <= pagingInfo.CurrentPage; i++)
                    result.Append(CreateLiTag(pagingInfo, i, pageUrl));

            if (pagingInfo.CurrentPage + 3 < pagingInfo.TotalPages - 3)
            {
                for (int i = pagingInfo.CurrentPage + 1; i <= pagingInfo.CurrentPage + 3; i++)
                    result.Append(CreateLiTag(pagingInfo, i, pageUrl));
                
                result.Append(CreateLinkSeparator());

                for (int i = pagingInfo.TotalPages - 1; i <= pagingInfo.TotalPages; i++)
                    result.Append(CreateLiTag(pagingInfo, i, pageUrl));
            }
            else
                for (int i = pagingInfo.CurrentPage + 1; i <= pagingInfo.TotalPages; i++)
                    result.Append(CreateLiTag(pagingInfo, i, pageUrl));

            return MvcHtmlString.Create(result.ToString());
        }

        private static string CreateLiTag(PagingInfo pagingInfo, int i, Func<int, string> pageUrl)
        {
            TagBuilder liTag = new TagBuilder("li");
            if (i == pagingInfo.CurrentPage)
                liTag.AddCssClass("active");
            TagBuilder aTag = new TagBuilder("a");
            aTag.MergeAttribute("href", pageUrl(i));
            aTag.InnerHtml = i.ToString();
            liTag.InnerHtml = aTag.ToString();
            return liTag.ToString();
        }

        private static string CreateLinkSeparator()
        {
            TagBuilder liTag = new TagBuilder("li");
            liTag.AddCssClass("disabled");
            TagBuilder aTag = new TagBuilder("a");
            aTag.MergeAttribute("href", "#");
            aTag.InnerHtml = "...";
            liTag.InnerHtml = aTag.ToString();
            return liTag.ToString();
        }
    }
}