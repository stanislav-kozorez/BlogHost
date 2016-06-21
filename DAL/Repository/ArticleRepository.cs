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

        public IEnumerable<DalArticle> GetPagedArticles(int page, int pageSize)
        {
            var ormArticles = Context.Set<Article>().Include("Author").OrderByDescending(x => x.CreationDate).Skip((page - 1) * pageSize).Take(pageSize).ToArray();
            return ormArticles.Select(x => Mapper.ToDal(x));
        }
    }
}
