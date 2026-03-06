using Babko_lab3.domain;
using NHibernate;

namespace Babko_lab3.dao;

public class GoodsDAO : GenericDAO<Goods>, IGoodsDAO
{
    public GoodsDAO(ISession session) : base(session) { }
}
