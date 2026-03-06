using Babko_lab3.domain;
using NHibernate;

namespace Babko_lab3.dao;

public class OrderDAO : GenericDAO<Order>, IOrderDAO
{
    public OrderDAO(ISession session) : base(session) { }
}
