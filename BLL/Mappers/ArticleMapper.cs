using BLL.Interface.Entities;
using DAL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class ArticleMapper
    {
        public static DalArticle ToDalArticle(this BllArticle article)
        {
            return new DalArticle()
            {
                Id = article.ArticleId,
                CreationDate = article.CreationDate,
                Tag1 = article.Tag1,
                Tag2 = article.Tag2,
                Tag3 = article.Tag3,
                Title = article.Title,
                Text = article.Text,
                Author = article.Author?.ToDalUser()
            };
        }

        public static BllArticle ToBllArticle(this DalArticle article)
        {
            return new BllArticle()
            {
                ArticleId = article.Id,
                CreationDate = article.CreationDate,
                Tag1 = article.Tag1,
                Tag2 = article.Tag2,
                Tag3 = article.Tag3,
                Title = article.Title,
                Text = article.Text,
                Author = article.Author?.ToBllUser()
            };
        }
    }
}
