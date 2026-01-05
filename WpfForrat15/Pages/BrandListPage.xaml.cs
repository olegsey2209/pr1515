using System;
using System.Collections.Generic;
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
using WpfForrat15.Models;
using WpfForrat15.Services;

namespace WpfForrat15.Pages
{
    /// <summary>
    /// Логика взаимодействия для BrandListPage.xaml
    /// </summary>
    public partial class BrandListPage : Page
    {
        private BrandService _brandService = new BrandService();
       
        private Brand _selectedBrand;
        public BrandListPage()
        {
            InitializeComponent();
            BrandsListView.ItemsSource = _brandService.Brands;
            Loaded += BrandListPage_Loaded;
        }
        private void BrandListPage_Loaded(object sender, RoutedEventArgs e)
        {
            
            _brandService.GetAll();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
          
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BrandFormPage());
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedBrand != null)
            {
                NavigationService.Navigate(new BrandFormPage(_selectedBrand));
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedBrand != null)
            {
                if (MessageBox.Show("Удалить бренд?", "Подтверждение",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _brandService.Remove(_selectedBrand);
                }
            }
        }

        private void BrandsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedBrand = BrandsListView.SelectedItem as Brand;
        }
    }
}
