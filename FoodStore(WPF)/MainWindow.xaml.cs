using System.Windows;
using System.Windows.Controls;

namespace FoodStore
{
    public partial class MainWindow : Window
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private List<Product> _currentProducts;

        public MainWindow()
        {
            InitializeComponent();
            _productService = new ProductService();
            _orderService = new OrderService();
            LoadProducts();
        }

        private void OnCategoryClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var category = button.Content.ToString();

            _currentProducts = _productService.GetProductsByCategory(category);
            ProductListView.ItemsSource = _currentProducts;
            ApplySort();
        }

        private void LoadProducts()
        {
            _currentProducts = _productService.GetAllProducts();
            ProductListView.ItemsSource = _currentProducts;
        }

        private void ApplySort()
        {
            if (SortComboBox.SelectedItem is ComboBoxItem selectedSortOption)
            {
                switch (selectedSortOption.Content.ToString())
                {
                    case "Name Ascending":
                        ProductListView.ItemsSource = _currentProducts.OrderBy(p => p.Name).ToList();
                        break;
                    case "Name Descending":
                        ProductListView.ItemsSource = _currentProducts.OrderByDescending(p => p.Name).ToList();
                        break;
                    case "Price Ascending":
                        ProductListView.ItemsSource = _currentProducts.OrderBy(p => p.Price).ToList();
                        break;
                    case "Price Descending":
                        ProductListView.ItemsSource = _currentProducts.OrderByDescending(p => p.Price).ToList();
                        break;
                }
            }
        }

        private void ProductListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedProduct = (Product)ProductListView.SelectedItem;
            if (selectedProduct != null)
            {
                var order = new Order
                {
                    ProductId = selectedProduct.Id,
                    ProductName = selectedProduct.Name,
                    Quantity = 1,
                    Price = selectedProduct.Price
                };

                _orderService.AddOrder(order);
                UpdateOrderList();
            }
        }
        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplySort();
        }


        private void IncreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var order = button.DataContext as Order;

            if (order != null)
            {
                _orderService.UpdateOrderQuantity(order.ProductId, 1);
                UpdateOrderList();
            }
        }

        private void DecreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var order = button.DataContext as Order;

            if (order != null)
            {
                _orderService.UpdateOrderQuantity(order.ProductId, -1);
                if (order.Quantity == 0)
                {
                    _orderService.RemoveOrder(order);
                }
                UpdateOrderList();
            }
        }

        private void RemoveOrder_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var order = button.DataContext as Order;

            if (order != null)
            {
                _orderService.RemoveOrder(order);
                UpdateOrderList();
            }
        }


        private void UpdateOrderList()
        {
            OrderListView.ItemsSource = null;
            OrderListView.ItemsSource = _orderService.GetAllOrders();
            UpdateTotal();
        }

        private void UpdateTotal()
        {
            var total = _orderService.GetAllOrders().Sum(o => o.Total);
            TotalTextBlock.Text = $"Total: {total:C}";
        }
    }
}