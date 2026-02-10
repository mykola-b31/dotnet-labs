using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Babko_lab2;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        IList<Goods> goodsList = GoodsRepository.GetInstance().GetAll();
        GoodsGrid.ItemsSource = goodsList;
        Application.Current.MainWindow.Closing +=
            new CancelEventHandler(MainWindow_Closing);
    }

    private void MenuItemEdit_Click(object sender, EventArgs e)
    {
        Goods goods = (Goods)GoodsGrid.SelectedItem;
        InputTextBox1.Text = goods.Id.ToString();
        InputTextBox2.Text = goods.Name;
        InputTextBox3.Text = goods.Category;
        InputTextBox4.Text = goods.Price.ToString();
        InputUnitComboBox.Text = goods.Unit;
        InputTextBox5.Text = goods.Quantity.ToString();
        AddButton.IsEnabled = false;
        EditButton.IsEnabled = true;
        CancelButton.IsEnabled = true;
    }

    private void MenuItemDelete_Click(object sender, EventArgs e)
    {
        string messageBoxText = "Do you want to delete goods?";
        string caption = "Delete goods";
        MessageBoxButton button = MessageBoxButton.OKCancel;
        MessageBoxImage icon = MessageBoxImage.Question;
        MessageBoxResult result = MessageBox.Show(messageBoxText, caption,
            button, icon, MessageBoxResult.Yes);
        if (result.Equals(MessageBoxResult.OK))
        {
            Goods goods = (Goods)GoodsGrid.SelectedItem;
            GoodsRepository.GetInstance().Delete(goods.Id);
            IList<Goods> goodsList = GoodsRepository.GetInstance().GetAll();
            GoodsGrid.ItemsSource = goodsList;
        }
    }

    private void ButtonAdd_Click(object sender, EventArgs e)
    {
        Goods goods = new Goods();
        goods.Name = InputTextBox2.Text;
        goods.Category = InputTextBox3.Text;
        if (decimal.TryParse(InputTextBox4.Text, out decimal price))
        {
            goods.Price = price;
        }
        else
        {
            MessageBox.Show("Input correct price!");
            return;
        }
        if (InputUnitComboBox.SelectedItem is ComboBoxItem selectedItem)
        {
            goods.Unit = selectedItem.Content.ToString();
        }
        
        if (Int32.TryParse(InputTextBox5.Text, out int quantity))
        {
            goods.Quantity = quantity;
        }
        else
        {
            MessageBox.Show("Input correct quantity!");
            return;
        }
        GoodsRepository.GetInstance().Save(goods);
        IList<Goods> goodsList = GoodsRepository.GetInstance().GetAll();
        GoodsGrid.ItemsSource = goodsList;
        InputTextBox1.Text = "";
        InputTextBox2.Text = "";
        InputTextBox3.Text = "";
        InputTextBox4.Text = "";
        InputTextBox5.Text = "";
    }

    private void ButtonEdit_Click(object sender, EventArgs e)
    {
        Goods goods = new Goods();
        goods.Id = Int64.Parse(InputTextBox1.Text);
        goods.Name = InputTextBox2.Text;
        goods.Category = InputTextBox3.Text;
        if (decimal.TryParse(InputTextBox4.Text, out decimal price))
        {
            goods.Price = price;
        }
        else
        {
            MessageBox.Show("Input correct price!");
            return;
        }
        if (InputUnitComboBox.SelectedItem is ComboBoxItem selectedItem)
        {
            goods.Unit = selectedItem.Content.ToString();
        }
        
        if (Int32.TryParse(InputTextBox5.Text, out int quantity))
        {
            goods.Quantity = quantity;
        }
        else
        {
            MessageBox.Show("Input correct quantity!");
            return;
        }
        GoodsRepository.GetInstance().Update(goods);
        IList<Goods> goodsList = GoodsRepository.GetInstance().GetAll();
        GoodsGrid.ItemsSource = goodsList;
        InputTextBox1.Text = "";
        InputTextBox2.Text = "";
        InputTextBox3.Text = "";
        InputTextBox4.Text = "";
        InputTextBox5.Text = "";
        EditButton.IsEnabled = false;
        CancelButton.IsEnabled = false;
        AddButton.IsEnabled = true;
    }

    private void ButtonCancel_Click(object sender, EventArgs e)
    {
        InputTextBox1.Text = "";
        InputTextBox2.Text = "";
        InputTextBox3.Text = "";
        InputTextBox4.Text = "";
        InputTextBox5.Text = "";
        EditButton.IsEnabled = false;
        CancelButton.IsEnabled = false;
        AddButton.IsEnabled = true;
    }

    void MainWindow_Closing(object sender, CancelEventArgs e)
    {
        GoodsRepository.GetInstance().Destroy();
    }
}