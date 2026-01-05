using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfForrat15.Models;

namespace WpfForrat15.Services
{
    public class CategoryService
    {
        private readonly Forrat158Context _db = BaseDbService.Instance.Context;
        public ObservableCollection<Category> Categories { get; set; } = new();

        public CategoryService()
        {
            GetAll();
        }

        public void GetAll()
        {
            var categories = _db.Categories.ToList();
            Categories.Clear();
            foreach (var category in categories)
                Categories.Add(category);
        }

        public void Add(Category category)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
            Categories.Add(category);
        }

        public void Remove(Category category)
        {
            _db.Categories.Remove(category);
            _db.SaveChanges();
            Categories.Remove(category);
        }

        public void Update(Category category)
        {
            _db.SaveChanges();
        }

        public bool IsNameUnique(string name, int? currentId = null)
        {
            return !_db.Categories.Any(c =>
                c.Name.ToLower() == name.ToLower() &&
                c.Id != (currentId ?? 0));
        }
    }
}
