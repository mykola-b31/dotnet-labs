using System.Windows;
using System.Windows.Controls;
using Babko_lab3.domain;
using NHibernate.Event;

namespace Babko_lab3;

public partial class GoodsWindow : Window
{
    private MainWindow parentWindow;

    public Goods Goods { get; set; }

    public GoodsWindow (MainWindow parentWindow)
    {
        this.parentWindow = parentWindow;
        InitializeComponent();
        RefreshGoodsGrid();
    }

    public MainWindow GetParentWindow()
    {
        return parentWindow;
    }

    private void RefreshGoodsGrid()
    {
        GoodsGrid.ItemsSource = GetParentWindow().GetDAOFactory().GetGoodsDAO().GetAll();
    }

    public void GoodsGrid_Click(object sender, EventArgs e)
    {
        if (GoodsGrid.SelectedItem is Goods selectedGoods)
        {
            this.Goods = selectedGoods;
            LoadGoodsToUI();
            DeleteButton.IsEnabled = true;
        }
    }

    public void ButtonSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(InputName.Text))
        {
            MessageBox.Show("Введіть назву товару!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        if (!decimal.TryParse(InputPrice.Text, out decimal price) || price < 0)
        {
            MessageBox.Show("Введіть коректну ціну!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        if (!int.TryParse(InputQuantity.Text, out int quantity) || quantity < 0)
        {
            MessageBox.Show("Введіть коректну кількість залишків!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        bool isNewGoods = (this.Goods == null || this.Goods.Id == 0);
        Goods currentGoods = isNewGoods ? new Goods() : GetParentWindow().GetDAOFactory().GetGoodsDAO().GetById(this.Goods.Id);

        currentGoods.Name = InputName.Text;
        currentGoods.Category = InputCategory.Text;
        currentGoods.Price = price;
        currentGoods.Quantity = quantity;
        
        if (ComboUnit.SelectedItem is ComboBoxItem selectedUnit)
        {
            currentGoods.Unit = selectedUnit.Content.ToString();
        }

        GetParentWindow().GetDAOFactory().GetGoodsDAO().SaveOrUpdate(currentGoods);

        RefreshGoodsGrid();
        
        this.Goods = null;
        LoadGoodsToUI();
        DeleteButton.IsEnabled = false;
        MessageBox.Show("Товар успішно збережено!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    public void LoadGoodsToUI()
    {
        if (this.Goods == null || this.Goods.Id == 0)
        {
            InputName.Text = "";
            InputCategory.Text = "";
            InputPrice.Text = "0";
            ComboUnit.SelectedIndex = 0;
            InputQuantity.Text = "0";
            return;
        }
        InputName.Text = this.Goods.Name;
        InputCategory.Text = this.Goods.Category;
        InputPrice.Text = this.Goods.Price.ToString();
        foreach(ComboBoxItem item in ComboUnit.Items)
        {
            if(item.Content.ToString() == this.Goods.Unit)
            {
                ComboUnit.SelectedItem = item;
                break;
            }
        }
        InputQuantity.Text = this.Goods.Quantity.ToString();
    }

    public void PopulateGoodsFromUI(Goods goods)
    {
        goods.Name = InputName.Text;
        goods.Category = InputCategory.Text;
        if(decimal.TryParse(InputPrice.Text, out decimal price))
        {
            goods.Price = price;
        }
        else
        {
            MessageBox.Show("Введіть коректну ціну!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        if (ComboUnit.SelectedItem is ComboBoxItem selectedUnit)
        {
            goods.Unit = selectedUnit.Content.ToString();
        }
        if(int.TryParse(InputQuantity.Text, out int quantity))
        {
            goods.Quantity = quantity;
        }
        else
        {
            MessageBox.Show("Введіть коректну кількість залишків!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    private void ButtonCancel_Click(object sender, EventArgs e)
    {
        this.Close();
    }

    public void ButtonDelete_Click(object sender, RoutedEventArgs e)
    {
        if (this.Goods != null && this.Goods.Id != 0)
        {
            var result = MessageBox.Show($"Ви дійсно хочете видалити товар '{this.Goods.Name}'?", "Підтвердження", MessageBoxButton.YesNo, MessageBoxImage.Question);
            
            if (result == MessageBoxResult.Yes)
            {
                GetParentWindow().GetDAOFactory().GetGoodsDAO().Delete(this.Goods);
                
                this.Goods = null;
                LoadGoodsToUI();
                RefreshGoodsGrid();
                DeleteButton.IsEnabled = false;
            }
        }
    }

}