using BLL.Interface.Entities;
using System.Collections.Generic;

namespace BLL.Interface.Services
{
    public interface IArticleService
    {
        BllArticle GetArticle(int id);
        IEnumerable<BllArticle> GetAllArticles();
        IEnumerable<BllArticle> GetPagedArticles(int page, int pageSize);
        IEnumerable<BllArticle> GetPagedArticles(int page, int pageSize, int userId);
        int GetArticleCount();
        int GetArticleCount(int userId);
        void CreateArticle(BllArticle article);
        void DeleteArticle(BllArticle article);
        void UpdateArticle(BllArticle article);
    }
}
