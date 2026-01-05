using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using WpfForrat15.Models;
using WpfForrat15.Services;

namespace WpfForrat15.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductFormPage.xaml
    /// </summary>
    public partial class ProductFormPage : Page
    {
        //private ProductService _productService = new ProductService();
        private CategoryService _categoryService = new CategoryService();
        private BrandService _brandService = new BrandService();
        private ProductService _productService;

        private Product _product = new Product();
        private bool _isEdit = false;
        public ProductFormPage(ProductService productService, Product editProduct = null)
        {
            InitializeComponent();
            _productService = productService;
            if (editProduct != null)
            {
                _product = editProduct;
                _isEdit = true;
            }
            else
            {
                
                //_product.Stock = 0;
                //_product.Price = 0;
                _product.CreatedAt = DateTime.Now;
            }

            DataContext = _product;

            CategoryCombo.ItemsSource = _categoryService.Categories;
            BrandCombo.ItemsSource = _brandService.Brands;
          
                if (!_isEdit) 
                {
                   
                    PriceBox.Text = "";
                    StockBox.Text = "";
                    RatingkBox.Text = "";
                }
          
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (PriceBox.Text == "" ||
                 StockBox.Text == "" ||
                 RatingkBox.Text == "" ||
                CategoryCombo.SelectedItem == null ||
                BrandCombo.SelectedItem == null ||
                string.IsNullOrWhiteSpace(_product.Name))
            {
                MessageBox.Show("Заполните все поля");
                return;
            }
            if (HasValidationErrors())
            {
                MessageBox.Show(
                    "Заполните все поля корректно",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            if (_isEdit)
            {
                _productService.Update(_product);
            }
            else
            {
                _productService.Add(_product);
            }

            NavigationService.GoBack();
        }
        private void ManageTags_Click(object sender, RoutedEventArgs e)
        {
            if (_product.Id == 0)
            {
                MessageBox.Show("Сначала сохраните товар", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NavigationService.Navigate(new ProductTagsPage(_product));
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (HasValidationErrors())
            {
                MessageBox.Show(
                    "Исправьте ошибки перед выходом",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }
            NavigationService.GoBack();
        }
        private bool HasValidationErrors()
        {
            return Validation.GetHasError(NameBox) ||
                   Validation.GetHasError(PriceBox) ||
                   Validation.GetHasError(RatingkBox) ||
                   Validation.GetHasError(StockBox) ||
                   CategoryCombo.SelectedItem == null ||
                   BrandCombo.SelectedItem == null;
        }

    }
    public class ZeroToEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            if (value is int i && i == 0)
                return "";

            if (value is double d && d == 0)
                return "";

            if (value is decimal m && m == 0)
                return "";

            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var text = value as string;

            if (string.IsNullOrWhiteSpace(text))
                return 0;

          
            text = text.Replace(',', '.');

            if (targetType == typeof(int))
            {
                if (int.TryParse(text, out int i))
                    return i;
                return 0;
            }

            if (targetType == typeof(double))
            {
                if (double.TryParse(
                        text,
                        NumberStyles.AllowDecimalPoint,
                        CultureInfo.InvariantCulture,
                        out double d))
                    return d;

                return 0;
            }

            if (targetType == typeof(decimal))
            {
                if (decimal.TryParse(
                        text,
                        NumberStyles.AllowDecimalPoint,
                        CultureInfo.InvariantCulture,
                        out decimal m))
                    return m;

                return 0;
            }

            return 0;
        }
    }
}
