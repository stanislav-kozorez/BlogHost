using DAL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.Repository
{
    public interface IArticleRepository: IRepository<DalArticle>
    {
        IEnumerable<DalArticle> GetPagedArticles(int page, int pageSize);
        IEnumerable<DalArticle> GetPagedArticles(int page, int pageSize, int userId);
        int GetArticleCount(int userId);
    }
}
