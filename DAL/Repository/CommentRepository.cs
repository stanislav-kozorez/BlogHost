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
    public class CommentRepository: Repository<DalComment, Comment>, ICommentRepository
    {
        public CommentRepository(DbContext context, IMapper<DalComment, Comment> mapper)
            :base(context, mapper) { }

        public override void Create(DalComment entity)
        {
            var comment = Mapper.ToOrm(entity);
            var article = Context.Set<Article>().Find(entity.Article.Id);
            var author = Context.Set<User>().Find(entity.Author.Id);
            comment.Article = article;
            comment.Author = author;
            Context.Set<Comment>().Add(comment);
        }

        public int GetCommentCount(int articleId)
        {
            return Context.Set<Comment>().Where(x => x.Article.ArticleId == articleId).Count();
        }

        public IEnumerable<DalComment> GetPagedComments(int page, int pageSize, int articleId)
        {
            var ormComments = Context.Set<Comment>().OrderByDescending(x => x.CreationDate).Where(x => x.Article.ArticleId == articleId).Skip((page - 1) * pageSize).Take(pageSize).ToArray();
            return ormComments.Select(x => Mapper.ToDal(x));
        }
    }
}
