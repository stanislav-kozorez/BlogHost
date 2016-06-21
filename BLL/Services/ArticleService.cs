using BLL.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using DAL.Interface.Repository;
using BLL.Mappers;

namespace BLL.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork uow;
        private readonly IArticleRepository articleRepository;

        public ArticleService(IUnitOfWork uow, IArticleRepository repository)
        {
            this.uow = uow;
            this.articleRepository = repository;
        }

        public void CreateArticle(BllArticle article)
        {
            articleRepository.Create(article.ToDalArticle());
            uow.Commit();
        }

        public void DeleteArticle(BllArticle article)
        {
            articleRepository.Delete(article.ToDalArticle());
            uow.Commit();
        }

        public IEnumerable<BllArticle> GetAllArticles()
        {
            return articleRepository.GetAll().Select(x => x.ToBllArticle());
        }

        public BllArticle GetArticle(int id)
        {
            return articleRepository.GetById(id)?.ToBllArticle();
        }

        public void UpdateArticle(BllArticle article)
        {
            articleRepository.Update(article.ToDalArticle());
            uow.Commit();
        }

        public IEnumerable<BllArticle> GetPagedArticles(int page, int pageSize)
        {
            return articleRepository.GetPagedArticles(page, pageSize).Select(x => x.ToBllArticle());
        }

        public int GetArticleCount()
        {
            return articleRepository.GetCount();
        }

        public IEnumerable<BllArticle> GetPagedArticles(int page, int pageSize, int userId)
        {
            return articleRepository.GetPagedArticles(page, pageSize, userId).Select(x => x.ToBllArticle());
        }

        public int GetArticleCount(int userId)
        {
            return articleRepository.GetArticleCount(userId);
        }
    }
}
