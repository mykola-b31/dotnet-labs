namespace Babko_lab3.domain;

public enum OrderStatus
{
    New,
    Completed,
    Cancelled
}

public enum PaymentMethod
{
    Cash,
    Card,
    Online
}

public class Order : EntityBase
{
    private IList<OrderItem> items = new List<OrderItem>();
    public virtual IList<OrderItem> Items
    {
        get { return items; }
        set { items = value; }
    }
    public virtual string CustomerName { get; set; }
    public virtual string CashierName { get; set; }
    public virtual DateTime OrderTime { get; set; } = DateTime.Now;
    public virtual OrderStatus Status { get; set; } = OrderStatus.New;
    public virtual PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Cash;
    public virtual decimal TotalPrice
    {
        get
        {
            return Items?.Sum(item => item.Price * item.Quantity) ?? 0;
        }
    }

    public virtual void AddItem(OrderItem item)
    {
        item.Order = this;
        this.Items.Add(item);
    }
}
