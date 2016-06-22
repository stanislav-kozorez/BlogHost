using BLL.Interface.Entities;
using System.Collections.Generic;

namespace BLL.Interface.Services
{
    public interface ICommentService
    {
        IEnumerable<BllComment> GetPagedComments(int page, int pageSize, int articleId);
        int GetCommentCount(int articleId);
        void CreateComment(BllComment comment);
        void DeleteComment(BllComment comment);
    }
}
