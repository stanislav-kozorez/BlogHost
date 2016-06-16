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
    public class UserMapper: IMapper<DalUser, User>
    { 
        public DalUser ToDal(User entity)
        {
            return new DalUser()
            {
                Id = entity.UserId,
                Name = entity.Name,
                Email = entity.Email,
                Password = entity.Password,
                CreationDate = entity.CreationDate,
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
                CreationDate = entity.CreationDate
            };
        }
    }
}
