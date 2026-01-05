using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfForrat15.Models;

namespace WpfForrat15.Services
{
    public class BrandService
    {
        private readonly Forrat158Context _db = BaseDbService.Instance.Context;
        public ObservableCollection<Brand> Brands { get; set; } = new();

        public BrandService()
        {
            GetAll();
        }

        public void GetAll()
        {
            var brands = _db.Brands.ToList();
            Brands.Clear();
            foreach (var brand in brands)
                Brands.Add(brand);
        }

        public void Add(Brand brand)
        {
            _db.Brands.Add(brand);
            _db.SaveChanges();
            Brands.Add(brand);
        }

        public void Remove(Brand brand)
        {
            //_db.Brands.Remove(brand);
            //_db.SaveChanges();
            //Brands.Remove(brand);
            try
            {
                _db.Database.ExecuteSqlRaw(
                    "UPDATE products SET brand_id = NULL WHERE brand_id = {0}",
                    brand.Id);

                _db.Brands.Remove(brand);
                _db.SaveChanges();

                Brands.Remove(brand);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Update(Brand brand)
        {
            _db.SaveChanges();
        }

        public bool IsNameUnique(string name, int? currentId = null)
        {
            return !_db.Brands.Any(b =>
                b.Name.ToLower() == name.ToLower() &&
                b.Id != (currentId ?? 0));
        }
    }
}
