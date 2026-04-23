using orm.Domain;
using NHibernate.Criterion;
using ISession = NHibernate.ISession;

namespace orm.Dao;

public class GoodsDAO : GenericDAO<Goods>, IGoodsDAO
{
    public GoodsDAO(ISession session) : base(session) { }

    public IList<Goods> SearchByCriteria(string searchQuery)
    {
        var criteria = session.CreateCriteria<Goods>();

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            criteria.Add(Restrictions.InsensitiveLike("Category", searchQuery, MatchMode.Anywhere));
        }

        return criteria.List<Goods>();
    }
}