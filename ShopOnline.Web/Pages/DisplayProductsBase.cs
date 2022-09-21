using Microsoft.AspNetCore.Components;
using ShopOnline.Models.DTOs;

namespace ShopOnline.Web.Pages
{
    public class DisplayProductsBase:ComponentBase
    {
        [Parameter]
        public IEnumerable<ProductDto> Products { get; set; }
    }
}
