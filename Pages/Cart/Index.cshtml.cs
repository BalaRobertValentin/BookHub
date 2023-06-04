using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookRental.Models;
using BookRental.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace BookRental.Pages.Cart
{
    public class IndexModel : PageModel
    {
        private readonly CartService _cartService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly EmailService _emailService;

        public IndexModel(CartService cartService, UserManager<ApplicationUser> userManager, EmailService emailService)
        {
            _cartService = cartService;
            _userManager = userManager;
            _emailService = emailService;
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

        public IActionResult OnPostUpdateQuantity(Guid cartItemId, int newQuantity)
        {
            _cartService.UpdateQuantity(cartItemId, newQuantity);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostCheckoutAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            CartItems = _cartService.GetReadOnlyCartItems();
            TotalCost = _cartService.GetTotalCost();

            // Prepare the checkout content for the email.
            string checkoutContent = PrepareCheckoutContent();

            // Send the checkout email to the user.
            await _emailService.SendEmailAsync(user.Email, "Your Checkout Details", checkoutContent);

            // Clear the cart after checkout.
            foreach (var item in CartItems)
            {
                _cartService.RemoveBook(item.Id);
            }

            return RedirectToPage("/Cart/CheckoutSuccess");
        }

        private string PrepareCheckoutContent()
        {
            StringBuilder content = new StringBuilder("Here are your checkout details:<br/><br/>");

            foreach (var item in CartItems)
            {
                content.AppendLine($"Title: {item.Book.Title} <br/>");
                content.AppendLine($"Quantity: {item.Quantity} <br/>");
                content.AppendLine($"Price: {item.Book.Price * item.Quantity}$<br/>");
                content.AppendLine("<br/><br/>");
            }

            content.AppendLine($"Total Cost: {TotalCost}$<br/>");

            return content.ToString();
        }
    }
}