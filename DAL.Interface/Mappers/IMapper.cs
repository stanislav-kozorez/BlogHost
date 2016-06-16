using DAL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.Mappers
{
    public interface IMapper<TDal, TOrm> where TDal: IEntity where TOrm: class
    {
        TDal ToDal(TOrm entity);
        TOrm ToOrm(TDal entity);
    }
}
