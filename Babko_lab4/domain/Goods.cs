namespace Babko_lab4.domain;

public class Goods : EntityBase
{
    public virtual string Name { get; set; }
    public virtual string Category { get; set; }
    public virtual decimal Price { get; set; }
    public virtual string Unit { get; set; }
    public virtual int Quantity { get; set; }
}
