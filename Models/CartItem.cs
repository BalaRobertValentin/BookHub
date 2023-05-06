using System;

namespace BookRental.Models
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public Books Book { get; set; }
        public int Quantity { get; set; }
    }
}
