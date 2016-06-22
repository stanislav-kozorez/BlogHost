using DAL.Interface.Entities;
using System.Collections.Generic;

namespace DAL.Interface.Repository
{
    public interface ICommentRepository: IRepository<DalComment>
    {
        IEnumerable<DalComment> GetPagedComments(int page, int pageSize, int articleId);
        int GetCommentCount(int articleId);
    }
}
