using System;
using System.Collections.Generic;

namespace WpfForrat15.Models;

public partial class ProductTag : ObservableObject
{
    private int? _productId;
    public int? ProductId
    {
        get => _productId;
        set => SetProperty(ref _productId, value);
    }

    private int? _tagId;
    public int? TagId
    {
        get => _tagId;
        set => SetProperty(ref _tagId, value);
    }

    private Product? _product;
    public virtual Product? Product
    {
        get => _product;
        set => SetProperty(ref _product, value);
    }

    private Tag? _tag;
    public virtual Tag? Tag
    {
        get => _tag;
        set => SetProperty(ref _tag, value);
    }
}
