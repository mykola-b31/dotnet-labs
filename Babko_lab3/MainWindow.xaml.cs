using System.ComponentModel;
using System.Windows;
using Babko_lab3.dao;
using Babko_lab3.domain;

namespace Babko_lab3;
public partial class MainWindow : Window
{
    private DAOFactory daoFactory;

    public DAOFactory GetDAOFactory()
    {
        if (null == daoFactory)
        {
            daoFactory = NHibernateDAOFactory.getInstance();
        }
        return daoFactory;
    }

    public MainWindow()
    {
        this.Closing += new CancelEventHandler(MainWindow_Closing);
        InitializeComponent();
        RefreshOrderGrid();
    }

    public void RefreshOrderGrid()
    {
        OrderGrid.ItemsSource = null;
        OrderGrid.ItemsSource = GetDAOFactory().GetOrderDAO().GetAll();
    }

    public void RefreshOrderItemGrid()
    {
        if (OrderGrid.SelectedItem is Order selectedOrder)
        {
            Order order = GetDAOFactory().GetOrderDAO().GetById(selectedOrder.Id);
            OrderItemGrid.ItemsSource = null;
            OrderItemGrid.ItemsSource = order.Items;
        }
        else
        {
            OrderItemGrid.ItemsSource = null;
        }
    }

    public void OrderGrid_Click(object sender, RoutedEventArgs e)
    {
        RefreshOrderItemGrid();
    }

    public void MenuItemAddOrder_Click(object sender, EventArgs e)
    {
        OrderWindow orderWindow = new OrderWindow(this);
        orderWindow.LoadOrderToUI();
        orderWindow.ShowDialog();
    }

    private void MenuItemEditOrder_Click(object sender, EventArgs e)
    {
        if (OrderGrid.SelectedItem is Order order)
        {
            OrderWindow orderWindow = new OrderWindow(this);
            orderWindow.Order = order;
            orderWindow.LoadOrderToUI();
            orderWindow.ShowDialog();
        }
        else
        {
            MessageBox.Show("Будь ласка, оберіть замовлення", "Нічого редагувати", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    private void MenuItemDeleteOrder_Click(object sender, EventArgs e)
    {
        if (OrderGrid.SelectedItem is Order order)
        {
            var result = MessageBox.Show($"Ви дійсно хочете видалити замовлення №{order.Id}?", "Підтвердження", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                GetDAOFactory().GetOrderDAO().Delete(order);
                RefreshOrderGrid();
                OrderItemGrid.ItemsSource = null;
            }
        }
        else
        {
            MessageBox.Show("Будь ласка, оберіть замовлення", "Нічого видаляти", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    private void MenuItemGoods_Click(object sender, EventArgs e)
    {
        GoodsWindow goodsWindow = new GoodsWindow(this);
        goodsWindow.ShowDialog();
    }

    private void MenuItemAddItem_Click(object sender, EventArgs e)
    {
        if (OrderGrid.SelectedItem is Order order)
        {
            OrderItemWindow orderItemWindow = new OrderItemWindow(this);
            orderItemWindow.Order = order;
            orderItemWindow.LoadOrderItemToUI();
            orderItemWindow.ShowDialog();
        }
        else
        {
            MessageBox.Show("Оберіть замовлення зліва, до якого потрібно додати товар", "Помилка", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    public void MenuItemEditItem_Click(object sender, EventArgs e)
    {
        Order order = (Order)OrderGrid.SelectedItem;
        OrderItem orderItem = (OrderItem)OrderItemGrid.SelectedItem;
        if (orderItem == null)
        {
            MessageBox.Show("Будь ласка, оберіть позицію товару", "Нічого редагувати", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        else
        {
            OrderItemWindow orderItemWindow = new OrderItemWindow(this);
            orderItemWindow.Order = order;
            orderItemWindow.OrderItem = orderItem;
            orderItemWindow.LoadOrderItemToUI();
            orderItemWindow.ShowDialog();
        }
    }
    
    private void MenuItemDeleteItem_Click(object sender, EventArgs e)
    {
        if (OrderItemGrid.SelectedItem is OrderItem orderItem)
        {
            var result = MessageBox.Show("Ви дійсно хочете видалити цю позицію?", "Підтвердження", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                GetDAOFactory().GetOrderItemDAO().Delete(orderItem);
                RefreshOrderItemGrid();
                RefreshOrderGrid();
            }
        }
        else
        {
            MessageBox.Show("Будь ласка, оберіть позицію", "Нічого видаляти", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    private void MainWindow_Closing(object sender, CancelEventArgs e)
    {
        GetDAOFactory().Destroy();
    }

}