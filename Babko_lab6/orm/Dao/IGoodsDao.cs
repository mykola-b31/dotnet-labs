using orm.Domain;

namespace orm.Dao;

public interface IGoodsDAO : IGenericDAO<Goods>
{
    IList<Goods> SearchByNativeSql(string searchQuery);        
}
