using System.Windows;
using System.Windows.Controls;
using Babko_lab3.domain;

namespace Babko_lab3;

public partial class OrderItemWindow : Window
{
    private MainWindow parentWindow;

    public Order Order { get; set; }
    public OrderItem OrderItem { get; set;}

    public OrderItemWindow(MainWindow parentWindow)
    {
        this.parentWindow = parentWindow;
        InitializeComponent();
        ComboGoods.ItemsSource = GetParentWindow().GetDAOFactory().GetGoodsDAO().GetAll();
    }

    public MainWindow GetParentWindow()
    {
        return parentWindow;
    }

    public void ButtonSave_Click(object sender, EventArgs e)
    {
        if (ComboGoods.SelectedItem == null)
        {
            MessageBox.Show("Оберіть товар!");
            return;
        }
        if (!int.TryParse(InputQuantity.Text, out int qty) || qty <= 0)
        {
            MessageBox.Show("Введіть коректну кількість (більше нуля)!");
            return;
        }

        bool isNewItem = (this.OrderItem == null || this.OrderItem.Id == 0);

        OrderItem currentItem;
        if (isNewItem)
        {
            currentItem = new OrderItem();
        }
        else
        {
            currentItem = GetParentWindow().GetDAOFactory().GetOrderItemDAO().GetById(this.OrderItem.Id);
        }

        PopulateOrderItemFromUI(currentItem);
        GetParentWindow().GetDAOFactory().GetOrderItemDAO().SaveOrUpdate(currentItem);
        
        if (isNewItem)
        {
            this.Order.Items.Add(currentItem);
        }
        
        GetParentWindow().RefreshOrderItemGrid();
        GetParentWindow().RefreshOrderGrid();
        this.Close();
    }

    public void PopulateOrderItemFromUI(OrderItem item)
    {
        item.Order = this.Order;
        if (ComboGoods.SelectedItem is Goods selectedGoods)
        {
            item.Goods = selectedGoods;
            item.Price = selectedGoods.Price;
        }
        item.Quantity = int.Parse(InputQuantity.Text);
    }

    public void LoadOrderItemToUI()
    {
        if (this.OrderItem == null || this.OrderItem.Id == 0)
        {
            InputQuantity.Text = "";
            ComboGoods.SelectedIndex = 0;
            InputPrice.Text = "0";
            return;
        }
        InputQuantity.Text = this.OrderItem.Quantity.ToString();
        InputPrice.Text = this.OrderItem.Price.ToString();
        if (this.OrderItem.Goods != null)
        {
            ComboGoods.SelectedItem = ComboGoods.Items.Cast<Goods>()
                .FirstOrDefault(g => g.Id == this.OrderItem.Goods.Id);
        }
    }

    private void ComboGoods_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ComboGoods.SelectedItem is Goods selectedGoods)
        {
            InputPrice.Text = selectedGoods.Price.ToString("C");
        }
        else
        {
            InputPrice.Text = "0";
        }
    }

    private void ButtonCancel_Click(object sender, EventArgs e)
    {
        this.Close();
    }

}