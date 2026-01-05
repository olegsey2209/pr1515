using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfForrat15.Models;
using WpfForrat15.Services;

namespace WpfForrat15.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page, INotifyPropertyChanged
    {
        public ProductService ProductService { get; set; } = new ProductService();
        public CategoryService CategoryService { get; set; } = new CategoryService();
        public BrandService BrandService { get; set; } = new BrandService();
        public TagService TagService { get; set; } = new TagService();


        private bool _isManagerMode;
        public bool IsManagerMode
        {
            get => _isManagerMode;
            set
            {
                _isManagerMode = value;
                OnPropertyChanged(nameof(IsManagerMode));
            }
        }

        public int TotalCount => ProductService.Products.Count;
        public int FilteredCount => ProductService.ProductsView.Cast<object>().Count();
        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        public MainPage()
        {
            InitializeComponent();
            DataContext = this;

            IsManagerMode = LoginPage.IsManagerMode;


            Loaded += MainPage_Loaded;
        }
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            BrandService.GetAll();
            CategoryService.GetAll();

            LoadFilters();
          

        
        }

        private void DeleteProduct(object sender, RoutedEventArgs e)
        {
            if (SelectedProduct == null)
            {
                MessageBox.Show("Выберите товар для удаления", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"Удалить товар '{SelectedProduct.Name}'?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    ProductService.Remove(SelectedProduct);
                    SelectedProduct = null;
                    MessageBox.Show("Товар удален", "Успешно",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            UpdateCounts();
        }
        private void LoadFilters()
        {

            Dispatcher.BeginInvoke(new Action(() =>
            {
                try
                {
                    var allCategories = new ObservableCollection<Category>(CategoryService.Categories);
                    allCategories.Insert(0, new Category { Id = -1, Name = "Все категории" });
                    CategoryFilter.ItemsSource = allCategories;
                    CategoryFilter.SelectedIndex = 0;

                    var allBrands = new ObservableCollection<Brand>(BrandService.Brands);
                    allBrands.Insert(0, new Brand { Id = -1, Name = "Все бренды" });
                    BrandFilter.ItemsSource = allBrands;
                    BrandFilter.SelectedIndex = 0;


                    UpdateCounts();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке фильтров: {ex.Message}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }), DispatcherPriority.Background);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ProductService.RefreshFilters();
            UpdateCounts();
        }
        private void FilterChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoryFilter.SelectedItem is Category category)
                ProductService.CategoryFilterId = category.Id == -1 ? null : category.Id;

            if (BrandFilter.SelectedItem is Brand brand)
                ProductService.BrandFilterId = brand.Id == -1 ? null : brand.Id;

            ProductService.RefreshFilters();
            UpdateCounts();
        }
        private void PriceFilterChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(PriceFromBox.Text, out double from))
                ProductService.PriceFrom = from;
            else
                ProductService.PriceFrom = null;

            if (double.TryParse(PriceToBox.Text, out double to))
                ProductService.PriceTo = to;
            else
                ProductService.PriceTo = null;

            ProductService.RefreshFilters();
            UpdateCounts();
        }
        private void UpdateCounts()
        {
            OnPropertyChanged(nameof(TotalCount));
            OnPropertyChanged(nameof(FilteredCount));
        }
        private void ProductsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedProduct = ProductsList.SelectedItem as Product;
        }

        private void AddProduct(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new ProductFormPage());
            NavigationService.Navigate(new ProductFormPage(ProductService));
        }

        private void EditProduct(object sender, RoutedEventArgs e)
        {
            if (SelectedProduct != null && IsManagerMode)
            {
                //NavigationService.Navigate(new ProductFormPage(SelectedProduct));
                NavigationService.Navigate(new ProductFormPage(ProductService, SelectedProduct));
            }
        }

        private void ManageBrands(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BrandListPage());
        }

        private void ManageCategories(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CategoryListPage());
        }

        private void ManageTags(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TagListPage());
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SortComboBox.SelectedItem is ComboBoxItem item)
            {
                ProductService.ProductsView.SortDescriptions.Clear();

                switch (item.Tag as string)
                {
                    case "Name":
                        ProductService.ProductsView.SortDescriptions.Add(
                            new SortDescription("Name", ListSortDirection.Ascending));
                        break;
                    case "NameDesc":
                        ProductService.ProductsView.SortDescriptions.Add(
                            new SortDescription("Name", ListSortDirection.Descending));
                        break;
                    case "Price":
                        ProductService.ProductsView.SortDescriptions.Add(
                            new SortDescription("Price", ListSortDirection.Ascending));
                        break;
                    case "PriceDesc":
                        ProductService.ProductsView.SortDescriptions.Add(
                            new SortDescription("Price", ListSortDirection.Descending));
                        break;
                    case "Stock":
                        ProductService.ProductsView.SortDescriptions.Add(
                            new SortDescription("Stock", ListSortDirection.Ascending));
                        break;
                    case "StockDesc":
                        ProductService.ProductsView.SortDescriptions.Add(
                            new SortDescription("Stock", ListSortDirection.Descending));
                        break;
                }
                ProductService.ProductsView.Refresh();
                UpdateCounts();
            }
        }

        private void ResetFilters(object sender, RoutedEventArgs e)
        {
            ProductService.SearchQuery = "";
            ProductService.CategoryFilterId = null;
            ProductService.BrandFilterId = null;
            ProductService.PriceFrom = null;
            ProductService.PriceTo = null;

            CategoryFilter.SelectedIndex = 0;
            BrandFilter.SelectedIndex = 0;
            PriceFromBox.Text = "";
            PriceToBox.Text = "";
            SortComboBox.SelectedIndex = -1;

            ProductService.ClearSort();
            ProductService.RefreshFilters();
            UpdateCounts();
        }
    }
    public class LessThanTenConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int stock)
                return stock < 10;

            return false;
        }



        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
