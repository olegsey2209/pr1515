using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfForrat15.Models;

namespace WpfForrat15.Services
{
    public class TagService
    {
        private readonly Forrat158Context _db = BaseDbService.Instance.Context;
        public ObservableCollection<Tag> Tags { get; set; } = new();

        public TagService()
        {
            GetAll();
        }

        public void GetAll()
        {
            var tags = _db.Tags.ToList();
            Tags.Clear();
            foreach (var tag in tags)
                Tags.Add(tag);
        }

        public void Add(Tag tag)
        {
            _db.Tags.Add(tag);
            _db.SaveChanges();
            Tags.Add(tag);
        }

        public void Remove(Tag tag)
        {
            _db.Tags.Remove(tag);
            _db.SaveChanges();
            Tags.Remove(tag);
        }

        public void Update(Tag tag)
        {
            _db.SaveChanges();
        }

        public bool IsNameUnique(string name, int? currentId = null)
        {
            return !_db.Tags.Any(t =>
                t.Name.ToLower() == name.ToLower() &&
                t.Id != (currentId ?? 0));
        }
    }
}
