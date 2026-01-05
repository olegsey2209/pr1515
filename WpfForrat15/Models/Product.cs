using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfForrat15.Models;

public partial class Product : ObservableObject
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

    private string? _description;
    public string? Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    private double _price;
    public double Price
    {
        get => _price;
        set => SetProperty(ref _price, value);
    }

    private int _stock;
    public int Stock
    {
        get => _stock;
        set => SetProperty(ref _stock, value);
    }

    private double _rating;
    public double Rating
    {
        get => _rating;
        set => SetProperty(ref _rating, value);
    }

    private DateTime? _createdAt;
    public DateTime? CreatedAt
    {
        get => _createdAt;
        set => SetProperty(ref _createdAt, value);
    }

    private int? _categoryId;
    public int? CategoryId
    {
        get => _categoryId;
        set => SetProperty(ref _categoryId, value);
    }

    private int? _brandId;
    public int? BrandId
    {
        get => _brandId;
        set => SetProperty(ref _brandId, value);
    }

    private Category? _category;
    public virtual Category? Category
    {
        get => _category;
        set => SetProperty(ref _category, value);
    }

    private Brand? _brand;
    public virtual Brand? Brand
    {
        get => _brand;
        set => SetProperty(ref _brand, value);
    }

    [NotMapped]
    private string? _tags;

    [NotMapped]
    public string? Tags
    {
        get => _tags;
        set => SetProperty(ref _tags, value);
    }
}
