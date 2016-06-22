using DAL.Interface.Entities;
using DAL.Interface.Mappers;
using ORM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mappers
{
    public class CommentMapper : IMapper<DalComment, Comment>
    {
        private readonly IMapper<DalUser, User> userMapper;
        private readonly IMapper<DalArticle, Article> articleMapper;

        public CommentMapper(IMapper<DalUser, User> userMapper, IMapper<DalArticle, Article> articleMapper)
        {
            this.userMapper = userMapper;
            this.articleMapper = articleMapper;
        }

        public DalComment ToDal(Comment entity)
        {
            return new DalComment()
            {
                Id = entity.CommentId,
                CreationDate = entity.CreationDate,
                Text = entity.Text,
                Author = entity.Author == null? null : userMapper.ToDal(entity.Author),
                Article = entity.Article == null? null : articleMapper.ToDal(entity.Article)
            };
        }

        public Comment ToOrm(DalComment entity)
        {
            return new Comment()
            {
                CommentId = entity.Id,
                CreationDate = entity.CreationDate,
                Text = entity.Text,
                Author = entity.Author == null ? null : userMapper.ToOrm(entity.Author),
                Article = entity.Article == null ? null : articleMapper.ToOrm(entity.Article)
            };
        }
    }
}
