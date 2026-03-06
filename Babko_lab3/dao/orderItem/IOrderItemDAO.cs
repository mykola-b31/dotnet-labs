using System;
using Babko_lab3.domain;

namespace Babko_lab3.dao;

public interface IOrderItemDAO : IGenericDAO<OrderItem>
{
    IList<OrderItem> GetItemsByOrder(long orderId);
}
