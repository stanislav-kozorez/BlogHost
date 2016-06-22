using DAL.Interface.Entities;

namespace DAL.Interface.Mappers
{
    public interface IMapper<TDal, TOrm> where TDal: IEntity where TOrm: class
    {
        TDal ToDal(TOrm entity);
        TOrm ToOrm(TDal entity);
    }
}
