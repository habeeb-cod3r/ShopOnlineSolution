﻿using Microsoft.AspNetCore.Components;
using ShopOnline.Models.DTOs;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Pages
{
    public class ProductsBase:ComponentBase
    {
        [Inject]
        public IProductService ProductService { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Products = (IEnumerable<ProductDto>)await ProductService.GetItems();
        }
    }
}
