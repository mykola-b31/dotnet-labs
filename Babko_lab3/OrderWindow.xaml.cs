using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Babko_lab3.domain;

namespace Babko_lab3;

public partial class OrderWindow : Window
{
    private MainWindow parentWindow;

    public Order Order { get; set; }

    public OrderWindow(MainWindow parentWindow)
    {
        this.parentWindow = parentWindow;
        InitializeComponent();
    }

    public MainWindow GetParentWindow()
    {
        return parentWindow;
    }

    private void ButtonSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(InputCustomerName.Text))
        {
            MessageBox.Show("Будь ласка, введіть ім'я клієнта.", "Помилка валідації", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        
        if (string.IsNullOrWhiteSpace(InputCashierName.Text))
        {
            MessageBox.Show("Будь ласка, введіть ім'я касира.", "Помилка валідації", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        bool isNewOrder = (this.Order == null || this.Order.Id == 0);

        Order currentOrder;
        if (isNewOrder)
        {
            currentOrder = new Order();
        }
        else
        {
            currentOrder = GetParentWindow().GetDAOFactory().GetOrderDAO().GetById(this.Order.Id);
        }

        PopulateOrderFromUI(currentOrder);

        GetParentWindow().GetDAOFactory().GetOrderDAO().SaveOrUpdate(currentOrder);
        GetParentWindow().RefreshOrderGrid();
        this.Close();
    }

    public void PopulateOrderFromUI(Order order)
    {
        order.CustomerName = InputCustomerName.Text;
        order.CashierName = InputCashierName.Text;
        order.OrderTime = InputOrderDate.SelectedDate ?? DateTime.Now;

        if (ComboPaymentMethod.SelectedItem is ComboBoxItem selectedMethod)
        {
            order.PaymentMethod = Enum.Parse<PaymentMethod>(selectedMethod.Content.ToString());
        }
        if (ComboStatus.SelectedItem is ComboBoxItem selectedStatus)
        {
            order.Status = Enum.Parse<OrderStatus>(selectedStatus.Content.ToString());
        }
    }

    public void LoadOrderToUI()
    {
        if (this.Order == null || this.Order.Id == 0)
        {
            InputCustomerName.Text = "";
            InputCashierName.Text = "";
            InputOrderDate.SelectedDate = DateTime.Now;
            InputTotalPrice.Text = "0";
            ComboPaymentMethod.SelectedIndex = 0;
            ComboStatus.SelectedIndex = 0;
            return;
        }

        InputCustomerName.Text = this.Order.CustomerName;
        InputCashierName.Text = this.Order.CashierName;
        InputOrderDate.SelectedDate = this.Order.OrderTime;
        InputTotalPrice.Text = this.Order.TotalPrice.ToString("C");
        foreach (ComboBoxItem item in ComboPaymentMethod.Items)
        {
            if (item.Content.ToString() == this.Order.PaymentMethod.ToString())
            {
                ComboPaymentMethod.SelectedItem = item;
                break;
            }
        }

        foreach (ComboBoxItem item in ComboStatus.Items)
        {
            if (item.Content.ToString() == this.Order.Status.ToString())
            {
                ComboStatus.SelectedItem = item;
                break;
            }
        }
    }

    private void ButtonCancel_Click(object sender, EventArgs e)
    {
        this.Close();
    }

}