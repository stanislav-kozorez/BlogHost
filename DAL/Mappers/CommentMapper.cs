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

        public CommentMapper(IMapper<DalUser, User> mapper)
        {
            this.userMapper = mapper;
        }

        public DalComment ToDal(Comment entity)
        {
            return new DalComment()
            {
                Id = entity.CommentId,
                CreationDate = entity.CreationDate,
                Text = entity.Text,
                Author = entity.Author == null? null : userMapper.ToDal(entity.Author)
            };
        }

        public Comment ToOrm(DalComment entity)
        {
            return new Comment()
            {
                CommentId = entity.Id,
                CreationDate = entity.CreationDate,
                Text = entity.Text,
                Author = entity.Author == null ? null : userMapper.ToOrm(entity.Author)
            };
        }
    }
}
