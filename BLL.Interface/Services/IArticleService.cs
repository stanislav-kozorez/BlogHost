using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Services
{
    public interface IArticleService
    {
        BllArticle GetArticle(int id);
        IEnumerable<BllArticle> GetAllArticles();
        IEnumerable<BllArticle> GetPagedArticles(int page, int pageSize);
        int GetArticleCount();
        void CreateArticle(BllArticle article);
        void DeleteArticle(BllArticle article);
        void UpdateArticle(BllArticle article);
    }
}
