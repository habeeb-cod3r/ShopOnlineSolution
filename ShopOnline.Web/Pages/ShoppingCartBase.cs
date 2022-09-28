using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ShopOnline.Models.DTOs;
using ShopOnline.Web.Services;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Pages
{
    public class ShoppingCartBase:ComponentBase
    {
        [Inject]
        public IJSRuntime Js { get; set; }
        [Inject]
        public IShoppingCartService shoppingCartService { get; set; }
        public List<CartItemDto> ShoppingCartItems { get; set; }

        public string ErrorMessage { get; set; }
        public string TotalPrice { get; set; }
        public int TotalQuantity { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                ShoppingCartItems = await shoppingCartService.GetItems(HardCoded.UserId);
                CartChanged();
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
            }
        }
        protected async Task DeleteCartItem_Click(int id)
        {
            var cartItemDto = await shoppingCartService.DeleteItem(id);
            RemoveCartItem(id);
            CartChanged();
        }
        protected async Task UpdateQtyCartItem_Click(int id, int qty)
        {
            try
            {
                if (qty > 0) 
                {
                    var updateItemDto = new CartItemQtyUpdateDto
                    {
                        CartItemId = id,
                        Qty = qty
                    };
                    var returnedUpdateItemDto = await shoppingCartService.UpdateQty(updateItemDto);

                    UpdateItemTotalPrice(returnedUpdateItemDto);
                    CartChanged();
                    await Js.InvokeVoidAsync("MakeUpdateQtyButtonVisible", id, false);
                }
                else
                {
                    var item = ShoppingCartItems.FirstOrDefault(i => i.Id == id);
                    if (item == null)
                    {
                        item.Qty = 1;
                        item.TotalPrice = item.Price;
                    }
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected async Task UpdateQty_Input(int id)
        {
            await Js.InvokeVoidAsync("MakeUpdateQtyButtonVisible", id, true);
        }
        private void UpdateItemTotalPrice(CartItemDto cartItemDto)
        {
            var item = GetCartItem(cartItemDto.Id);
            if (item != null)
            {
                item.TotalPrice = cartItemDto.Price * cartItemDto.Qty;
            }
        }
        private void CalculateCartSummaryTotal()
        {
            SetTotalPrice();
            SetTotalQuantity();
        }
        private void SetTotalPrice()
        {
            TotalPrice = ShoppingCartItems.Sum(i => i.TotalPrice).ToString("C");
        }
        private void SetTotalQuantity()
        {
            TotalQuantity = ShoppingCartItems.Sum(p => p.Qty);
        }
        private CartItemDto GetCartItem(int id)
        {
            return ShoppingCartItems.FirstOrDefault(c => c.Id == id);
        }
        private void RemoveCartItem(int id)
        {
            var cartItemDto = GetCartItem(id);
            ShoppingCartItems.Remove(cartItemDto);
        }
        private void CartChanged()
        {
            CalculateCartSummaryTotal();
            shoppingCartService.RaiseEventOnShppingCartChanged(TotalQuantity);
        }
    }
}

