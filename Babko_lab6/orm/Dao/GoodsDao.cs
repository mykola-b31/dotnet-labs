using orm.Domain;
using NHibernate.Criterion;
using ISession = NHibernate.ISession;

namespace orm.Dao;

public class GoodsDAO : GenericDAO<Goods>, IGoodsDAO
{
    public GoodsDAO(ISession session) : base(session) { }

    public IList<Goods> SearchByNativeSql(string searchQuery)
    {
        string sql = "SELECT * FROM Goods WHERE Category LIKE :search";

        var query = session.CreateSQLQuery(sql)
            .AddEntity(typeof(Goods))
            .SetParameter("search", "%" + searchQuery + "%");

        return query.List<Goods>();
    }
}