using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookRental.Models;
using BookRental.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookRental.Pages.Cart
{
    public class IndexModel : PageModel
    {
        private readonly CartService _cartService;

        public IndexModel(CartService cartService)
        {
            _cartService = cartService;
        }

        public IReadOnlyList<CartItem> CartItems { get; set; }
        public decimal TotalCost { get; set; }

        public void OnGet()
        {
            CartItems = _cartService.GetReadOnlyCartItems();
            TotalCost = _cartService.GetTotalCost();
        }

        public IActionResult OnPostRemoveFromCart(Guid cartItemId)
        {
            _cartService.RemoveBook(cartItemId);
            return RedirectToPage();
        }
    }
}
