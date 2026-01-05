using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WpfForrat15.Models;

public partial class Brand : ObservableObject
{
    private int _id;
    public int Id
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    private string? _name;
    public string? Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }
    private ObservableCollection<Product> _products = new();
    public virtual ICollection<Product> Products
    {
        get => _products;
        set => SetProperty(ref _products, value as ObservableCollection<Product> ?? new ObservableCollection<Product>());
    }

}