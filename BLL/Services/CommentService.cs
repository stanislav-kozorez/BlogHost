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
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork uow;
        private readonly ICommentRepository commentRepository;

        public CommentService(ICommentRepository commentRepository, IUnitOfWork uow)
        {
            this.commentRepository = commentRepository;
            this.uow = uow;
        }

        public BllComment GetComment(int commentId)
        {
            return commentRepository.GetById(commentId)?.ToBllComment();
        }

        public void CreateComment(BllComment comment)
        {
            commentRepository.Create(comment.ToDalComment());
            uow.Commit();
        }

        public void DeleteComment(BllComment comment)
        {
            commentRepository.Delete(comment.ToDalComment());
            uow.Commit();
        }

        public int GetCommentCount(int articleId)
        {
            return commentRepository.GetCommentCount(articleId);
        }

        public IEnumerable<BllComment> GetPagedComments(int page, int pageSize, int articleId)
        {
            return commentRepository.GetPagedComments(page, pageSize, articleId).Select(x => x.ToBllComment());
        }
    }
}
