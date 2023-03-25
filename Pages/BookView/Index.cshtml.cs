using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookRental.Data;
using BookRental.Models;

namespace BookRental.Pages.Book
{
    public class IndexModel : PageModel
    {
        private readonly BookRental.Data.BookRentalContext _context;

        public IndexModel(BookRental.Data.BookRentalContext context)
        {
            _context = context;
        }

        public IList<Books> Books { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Books != null)
            {
                Books = await _context.Books.ToListAsync();
            }
        }
    }
}
