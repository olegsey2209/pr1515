using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для ProductTagsPage.xaml
    /// </summary>
    public partial class ProductTagsPage : Page
    {
        private readonly Product _product;
        private readonly TagService _tagService = new TagService();
        private readonly Forrat158Context _db = BaseDbService.Instance.Context;

        public ObservableCollection<TagItem> Tags { get; set; }
            = new ObservableCollection<TagItem>();
        public ProductTagsPage(Product product)
        {
            InitializeComponent();
            _product = product;

            LoadTags();
            TagsList.ItemsSource = Tags;
        }
        private void LoadTags()
        {
            var selectedTagIds = _db.ProductTags
                .Where(pt => pt.ProductId == _product.Id)
                .Select(pt => pt.TagId)
                .ToList();

            Tags.Clear();

            foreach (var tag in _tagService.Tags)
            {
                Tags.Add(new TagItem
                {
                    TagId = tag.Id,
                    Name = tag.Name,
                    IsSelected = selectedTagIds.Contains(tag.Id)
                });
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var db = BaseDbService.Instance.Context;

          
            db.Database.ExecuteSqlRaw(
                "DELETE FROM product_tags WHERE product_id = {0}",
                _product.Id);

         
            foreach (var tag in Tags.Where(t => t.IsSelected))
            {
                db.Database.ExecuteSqlRaw(
                    "INSERT INTO product_tags (product_id, tag_id) VALUES ({0}, {1})",
                    _product.Id,
                    tag.TagId);
            }

         
            var tagNames = db.ProductTags
                .Where(pt => pt.ProductId == _product.Id)
                .Select(pt => pt.Tag.Name)
                .ToList();

            _product.Tags = string.Join(" ", tagNames.Select(t => "#" + t));

            NavigationService.GoBack();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }

    public class TagItem : ObservableObject
    {
        public int TagId { get; set; }
        public string Name { get; set; }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }
    }
}

