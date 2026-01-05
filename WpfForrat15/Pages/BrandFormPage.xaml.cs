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
    /// Логика взаимодействия для BrandFormPage.xaml
    /// </summary>
    public partial class BrandFormPage : Page
    {
        private BrandService _brandService = new BrandService();
        private Brand _brand = new Brand();
        private bool _isEdit = false;
        public BrandFormPage(Brand editBrand = null)
        {
            InitializeComponent();
            if (editBrand != null)
            {
                _brand = editBrand;
                _isEdit = true;
            }

            DataContext = _brand;
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_brand.Name))
            {
                MessageBox.Show("Введите название бренда");
                return;
            }

            if (_isEdit)
            {
                _brandService.Update(_brand);
            }
            else
            {
                _brandService.Add(_brand);
            }

            NavigationService.GoBack();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
