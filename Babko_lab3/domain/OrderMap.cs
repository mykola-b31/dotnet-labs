using FluentNHibernate.Mapping;

namespace Babko_lab3.domain;

public class OrderMap : ClassMap<Order>
{
    public OrderMap()
    {
        Table("Orders");
        Id(x => x.Id).GeneratedBy.Native();
        Map(x => x.CustomerName);
        Map(x => x.CashierName);
        Map(x => x.OrderTime);
        Map(x => x.Status);
        Map(x => x.PaymentMethod);
        HasMany(x => x.Items)
            .Cascade.All()
            .Inverse()
            .KeyColumn("OrderId");
    }
}
