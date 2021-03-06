﻿using DAL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.Repository
{
    public interface IUserRepository: IRepository<DalUser>
    {
        IEnumerable<DalUser> GetPagedUsers(int page, int pageSize);
    }
}
