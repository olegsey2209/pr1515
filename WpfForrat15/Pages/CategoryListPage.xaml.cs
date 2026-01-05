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
    /// Логика взаимодействия для CategoryListPage.xaml
    /// </summary>
    public partial class CategoryListPage : Page
    {
        private CategoryService _categoryService = new CategoryService();
        private Category _selectedCategory;
        public CategoryListPage()
        {
            InitializeComponent();
            CategoriesListView.ItemsSource = _categoryService.Categories;
            Loaded += CategoryListPage_Loaded;
        }
        private void CategoryListPage_Loaded(object sender, RoutedEventArgs e)
        {
            _categoryService.GetAll();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CategoryFormPage());
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedCategory != null)
            {
                NavigationService.Navigate(new CategoryFormPage(_selectedCategory));
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedCategory != null)
            {
                if (MessageBox.Show("Удалить категорию?", "Подтверждение",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _categoryService.Remove(_selectedCategory);
                }
            }
        }
        private void CategoriesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedCategory = CategoriesListView.SelectedItem as Category;
        }
    }
}
