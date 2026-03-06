using Babko_lab3.domain;
using NHibernate;

namespace Babko_lab3.dao;

public class OrderItemDAO : GenericDAO<OrderItem>, IOrderItemDAO
{
    public OrderItemDAO(ISession session) : base(session) { }

    public IList<OrderItem> GetItemsByOrder(long orderId)
    {
        return session.Query<OrderItem>()
                  .Where(x => x.Order.Id == orderId)
                  .ToList();
    }
}
