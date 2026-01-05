using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using WpfForrat15.Models;

namespace WpfForrat15.Services
{
    public class ProductService
    {
        private readonly Forrat158Context _db = BaseDbService.Instance.Context;
        public ObservableCollection<Product> Products { get; set; } = new();
        public ICollectionView ProductsView { get; set; }

        public string SearchQuery { get; set; } = "";
        public int? CategoryFilterId { get; set; }
        public int? BrandFilterId { get; set; }
        public double? PriceFrom { get; set; }
        public double? PriceTo { get; set; }

        public ProductService()
        {
            GetAll();
            ProductsView = CollectionViewSource.GetDefaultView(Products);
            ProductsView.Filter = FilterProducts;
        }

        public void GetAll()
        {
            var products = _db.Products.ToList();
            var categories = _db.Categories.ToList();
            var brands = _db.Brands.ToList();


            foreach (var product in products)
            {
                if (product.CategoryId.HasValue)
                {
                    product.Category = categories.FirstOrDefault(c => c.Id == product.CategoryId.Value);
                }
                if (product.BrandId.HasValue)
                {
                    product.Brand = brands.FirstOrDefault(b => b.Id == product.BrandId.Value);
                }
            }
            foreach (var product in products)
            {
                var tagNames = _db.ProductTags
                    .Where(pt => pt.ProductId == product.Id)
                    .Select(pt => pt.Tag.Name)
                    .ToList();

                product.Tags = string.Join(" ", tagNames.Select(t => "#" + t));
            }

            Products.Clear();
            foreach (var product in products)
            {
                Products.Add(product);
            }
        }

        private bool FilterProducts(object obj)
        {
            if (obj is not Product product) return false;

            if (!string.IsNullOrEmpty(SearchQuery) &&
                !product.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase))
                return false;

            if (CategoryFilterId.HasValue && product.CategoryId != CategoryFilterId.Value)
                return false;

            if (BrandFilterId.HasValue && product.BrandId != BrandFilterId.Value)
                return false;

            if (PriceFrom.HasValue && product.Price < PriceFrom.Value)
                return false;
            if (PriceTo.HasValue && product.Price > PriceTo.Value)
                return false;

            return true;
        }

        public void RefreshFilters()
        {
            ProductsView.Refresh();
        }

        public void ApplySort(string property, ListSortDirection direction)
        {
            ProductsView.SortDescriptions.Clear();
            ProductsView.SortDescriptions.Add(new SortDescription(property, direction));
            ProductsView.Refresh();
        }

        public void ClearSort()
        {
            ProductsView.SortDescriptions.Clear();
            ProductsView.Refresh();
        }

        public void Add(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
            Products.Add(product);
            RefreshFilters();
        }

        public void Remove(Product product)
        {
            try
            {
              
                _db.Database.ExecuteSqlRaw(
                    "DELETE FROM product_tags WHERE product_id = {0}",
                    product.Id);            
                _db.Products.Remove(product);             
                _db.SaveChanges();               
                Products.Remove(product);
                RefreshFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении товара: {ex.Message}",
                               "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        public void Update(Product product)
        {
            _db.SaveChanges();
            RefreshFilters();
        }
    }
}
