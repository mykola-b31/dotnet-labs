using Babko_lab4.domain;
using ISession = NHibernate.ISession;

namespace Babko_lab4.dao;

public class GoodsDAO : GenericDAO<Goods>, IGoodsDAO
{
    public GoodsDAO(ISession session) : base(session) { }
}