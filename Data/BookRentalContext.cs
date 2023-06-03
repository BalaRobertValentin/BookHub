using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookRental.Models;

namespace BookRental.Data
{
    public class BookRentalContext : DbContext
    {
        public BookRentalContext (DbContextOptions<BookRentalContext> options)
            : base(options)
        {
        }

        public DbSet<Books> Books { get; set; } = default!;

        public DbSet<Contact> Contact { get; set; } = default!;

        public DbSet<FavoriteItem> FavoriteItems { get; set; }
    }
}
