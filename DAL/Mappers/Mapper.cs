using DAL.Interface.Entities;
using ORM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL.Mappers
{
    public static class Mapper
    {
        public static DalUser ToDalUser(this User ormUser)
        {
            return new DalUser()
            {
                Id = ormUser.UserId,
                Name = ormUser.Name,
                Email = ormUser.Email,
                Password = ormUser.Password,
                CreationDate = ormUser.CreationDate,
            };
        }
        
        public static User ToOrmUser(this DalUser dalUser)
        {
            return new User()
            {
                UserId = dalUser.Id,
                Name = dalUser.Name,
                Email = dalUser.Email,
                Password = dalUser.Password,
                CreationDate = dalUser.CreationDate
            };
        }
    }
}
