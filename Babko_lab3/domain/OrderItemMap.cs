using System;
using FluentNHibernate.Mapping;

namespace Babko_lab3.domain;

public class OrderItemMap : ClassMap<OrderItem>
{
    public OrderItemMap()
    {
        Table("OrderItems");
        Id(x => x.Id).GeneratedBy.Native();
        
        Map(x => x.Quantity);
        Map(x => x.Price);

        References(x => x.Order).Column("OrderId");
        References(x => x.Goods).Column("GoodsId");
    }
}
