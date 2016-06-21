using DAL.Interface.Mappers;
using DAL.Interface.Entities;
using ORM.Entity;

namespace DAL.Mappers
{
    public class ArticleMapper : IMapper<DalArticle, Article>
    {
        private readonly IMapper<DalUser, User> userMapper;

        public ArticleMapper(IMapper<DalUser, User> mapper)
        {
            this.userMapper = mapper;
        }

        public DalArticle ToDal(Article entity)
        {
            return new DalArticle()
            {
                Id = entity.ArticleId,
                CreationDate = entity.CreationDate,
                Tag1 = entity.Tag1,
                Tag2 = entity.Tag2,
                Tag3 = entity.Tag3,
                Title = entity.Title,
                Text = entity.Text,
                Author = userMapper.ToDal(entity.Author)
            };
        }

        public Article ToOrm(DalArticle entity)
        {
            return new Article()
            {
                ArticleId = entity.Id,
                CreationDate = entity.CreationDate,
                Tag1 = entity.Tag1,
                Tag2 = entity.Tag2,
                Tag3 = entity.Tag3,
                Title = entity.Title,
                Text = entity.Text,
                Author = userMapper.ToOrm(entity.Author)
            };
        }
    }
}
