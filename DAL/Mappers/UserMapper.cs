using DAL.Interface.Entities;
using DAL.Interface.Mappers;
using DAL.Interface.Repository;
using ORM.Entity;

namespace DAL.Mappers
{
    public class UserMapper: IMapper<DalUser, User>
    {
        private IMapper<DalRole, Role> roleMapper;

        public UserMapper(IMapper<DalRole, Role> mapper)
        {
            this.roleMapper = mapper;
        }

        public DalUser ToDal(User entity)
        {
            return new DalUser()
            {
                Id = entity.UserId,
                Name = entity.Name,
                Email = entity.Email,
                Password = entity.Password,
                CreationDate = entity.CreationDate,
                Role = roleMapper.ToDal(entity.Role)
            };
        }

        public User ToOrm(DalUser entity)
        {
            return new User()
            {
                UserId = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                Password = entity.Password,
                CreationDate = entity.CreationDate,
                Role = roleMapper.ToOrm(entity.Role)
            };
        }
    }
}
