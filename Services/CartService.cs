using BookRental.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookRental.Services
{
    public class CartService
    {
        private const string CartSessionKey = "CartItems";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private List<CartItem> GetCartItems()
        {
            var sessionData = _httpContextAccessor.HttpContext.Session.GetString(CartSessionKey);
            return string.IsNullOrEmpty(sessionData) ? new List<CartItem>() : JsonConvert.DeserializeObject<List<CartItem>>(sessionData);
        }

        private void SetCartItems(List<CartItem> cartItems)
        {
            var sessionData = JsonConvert.SerializeObject(cartItems);
            _httpContextAccessor.HttpContext.Session.SetString(CartSessionKey, sessionData);
        }

        public void AddBook(Books book, int quantity)
        {
            var cartItems = GetCartItems();
            var existingItem = cartItems.SingleOrDefault(c => c.Book.Id == book.Id);

            if (existingItem == null)
            {
                cartItems.Add(new CartItem { Book = book, Quantity = quantity, Id = Guid.NewGuid() });
            }
            else
            {
                existingItem.Quantity += quantity;
            }

            SetCartItems(cartItems);
        }

        public void RemoveBook(Guid cartItemId)
        {
            var cartItems = GetCartItems();
            var cartItem = cartItems.SingleOrDefault(c => c.Id == cartItemId);
            if (cartItem != null)
            {
                cartItems.Remove(cartItem);
            }

            SetCartItems(cartItems);
        }

        public decimal GetTotalCost()
        {
            var cartItems = GetCartItems();
            return cartItems.Sum(c => (decimal)c.Book.Price * c.Quantity);
        }

        public IReadOnlyList<CartItem> GetReadOnlyCartItems()
        {
            return GetCartItems().AsReadOnly();
        }
    }
}
