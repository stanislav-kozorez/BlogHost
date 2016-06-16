using BLL.Interface.Services;
using BLL.Services;
using DAL.Interface.Entities;
using DAL.Interface.Mappers;
using DAL.Interface.Repository;
using DAL.Mappers;
using DAL.Repository;
using Ninject;
using Ninject.Web.Common;
using ORM;
using ORM.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyResolver
{
    public static class ResolverConfig
    {
        public static void AddBindings(this IKernel kernel)
        {
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            kernel.Bind<DbContext>().To<BlogHostDbContext>().InRequestScope();

            kernel.Bind<IUserRepository>().To<UserRepository>().InRequestScope();
            kernel.Bind<IRoleRepository>().To<RoleRepository>().InRequestScope();

            kernel.Bind<IMapper<DalUser, User>>().To<UserMapper>().InSingletonScope();
            kernel.Bind<IMapper<DalRole, Role>>().To<RoleMapper>().InSingletonScope();

            kernel.Bind<IUserService>().To<UserService>().InRequestScope();
            kernel.Bind<IRoleService>().To<RoleService>().InRequestScope();
        }
    }
}
