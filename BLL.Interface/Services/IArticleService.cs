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
        IEnumerable<BllArticle> FindByKeyword(string keyword, int page, int pageSize);
        int FindByKeywordCount(string keyword);
        IEnumerable<BllArticle> FindByTag(string tagName, int page, int pageSize);
        int FindByTagCount(string tagName);
        int GetArticleCount();
        int GetArticleCount(int userId);
        void CreateArticle(BllArticle article);
        void DeleteArticle(BllArticle article);
        void UpdateArticle(BllArticle article);
    }
}
