using DAL.Interface.Entities;
using DAL.Interface.Mappers;
using DAL.Interface.Repository;
using ORM.Entity;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repository
{
    public class ArticleRepository: Repository<DalArticle, Article>, IArticleRepository
    {
        public ArticleRepository(DbContext context, IMapper<DalArticle, Article> mapper)
            :base(context, mapper) { }

        public override void Create(DalArticle entity)
        {
            var article = Mapper.ToOrm(entity);
            var user = Context.Set<User>().Find(article.Author.UserId);
            article.Author = user;
            Context.Set<Article>().Add(article);
        }

        public override void Update(DalArticle entity)
        {
            var article = Context.Set<Article>().Find(entity.Id);
            if (article == null)
                throw new ArgumentException($"The entity with id = {entity.Id} doesn't exist");
            
            article.Tag1 = entity.Tag1;
            article.Tag2 = entity.Tag2;
            article.Tag3 = entity.Tag3;
            article.Title = entity.Title;
            article.Text = entity.Text;
    }

        public int GetArticleCount(int userId)
        {
            return Context.Set<Article>().Where(x => x.Author.UserId == userId).Count();
        }

        public IEnumerable<DalArticle> GetPagedArticles(int page, int pageSize)
        {
            var ormArticles = Context.Set<Article>().Include("Author").OrderByDescending(x => x.CreationDate).Skip((page - 1) * pageSize).Take(pageSize).ToArray();
            return ormArticles.Select(x => Mapper.ToDal(x));
        }

        public IEnumerable<DalArticle> GetPagedArticles(int page, int pageSize, int userId)
        {
            var ormArticles = Context.Set<Article>().OrderByDescending(x => x.CreationDate).Where(x => x.Author.UserId == userId).Skip((page - 1) * pageSize).Take(pageSize).ToArray();
            return ormArticles.Select(x => Mapper.ToDal(x));
        }
    }
}
