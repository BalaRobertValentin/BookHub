using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookRental.Models;
using WingtipToys.Models;

namespace BookRental.Data
{
    public class BookRentalContext : DbContext
    {
        public BookRentalContext (DbContextOptions<BookRentalContext> options)
            : base(options)
        {
        }

        public DbSet<BookRental.Models.Books> Books { get; set; } = default!;

        public DbSet<BookRental.Models.Contact> Contact { get; set; } = default!;
        public DbSet<BookRental.Models.Client> Client { get; set; } = default!;
        public DbSet<CartItem> ShoppingCartItems { get; set; }
    }
}
