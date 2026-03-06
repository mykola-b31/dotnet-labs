using System;

namespace Babko_lab3.domain;

public class OrderItem : EntityBase
{
    public virtual Order Order { get; set; }
    
    public virtual Goods Goods { get; set; }

    public virtual int Quantity { get; set; }

    public virtual decimal Price { get; set; }
}
