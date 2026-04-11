using Babko_lab5.Domain;
using ISession = NHibernate.ISession;

namespace Babko_lab5.Dao;

public class GoodsDAO : GenericDAO<Goods>, IGoodsDAO
{
    public GoodsDAO(ISession session) : base(session) { }
}