using Babko_lab5.Domain;
using NHibernate.Criterion;
using ISession = NHibernate.ISession;

namespace Babko_lab5.Dao;

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