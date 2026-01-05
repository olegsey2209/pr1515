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
    /// Логика взаимодействия для TagFormPage.xaml
    /// </summary>
    public partial class TagFormPage : Page
    {
        private TagService _tagService = new TagService();
        private Tag _tag = new Tag();
        private bool _isEdit = false;
        public TagFormPage(Tag editTag = null)
        {
            InitializeComponent();
            if (editTag != null)
            {
                _tag = editTag;
                _isEdit = true;
            }

            DataContext = _tag;
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_tag.Name))
            {
                MessageBox.Show("Введите название тега");
                return;
            }

            if (_isEdit)
            {
                _tagService.Update(_tag);
            }
            else
            {
                _tagService.Add(_tag);
            }

            NavigationService.GoBack();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
