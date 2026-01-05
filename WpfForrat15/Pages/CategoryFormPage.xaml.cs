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
    /// Логика взаимодействия для CategoryFormPage.xaml
    /// </summary>
    public partial class CategoryFormPage : Page
    {
        private CategoryService _categoryService = new CategoryService();
        private Category _category = new Category();
        private bool _isEdit = false;
        public CategoryFormPage(Category editCategory = null)
        {
            InitializeComponent();
            if (editCategory != null)
            {
                _category = editCategory;
                _isEdit = true;
            }

            DataContext = _category;
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_category.Name))
            {
                MessageBox.Show("Введите название категории");
                return;
            }

            if (_isEdit)
            {
                _categoryService.Update(_category);
            }
            else
            {
                _categoryService.Add(_category);
            }

            NavigationService.GoBack();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
