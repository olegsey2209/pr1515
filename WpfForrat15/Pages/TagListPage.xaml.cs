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
    /// Логика взаимодействия для TagListPage.xaml
    /// </summary>
    public partial class TagListPage : Page
    {
        private TagService _tagService = new TagService();
        private Tag _selectedTag;
        public TagListPage()
        {
            InitializeComponent();
            TagsListView.ItemsSource = _tagService.Tags;
            Loaded += TagListPage_Loaded;
        }
        private void TagListPage_Loaded(object sender, RoutedEventArgs e)
        {
            _tagService.GetAll();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
           
            NavigationService.Navigate(new MainPage());
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TagFormPage());
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedTag != null)
            {
                NavigationService.Navigate(new TagFormPage(_selectedTag));
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedTag != null)
            {
                if (MessageBox.Show("Удалить тег?", "Подтверждение",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _tagService.Remove(_selectedTag);
                }
            }
        }
        private void TagsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedTag = TagsListView.SelectedItem as Tag;
        }
    }
}
