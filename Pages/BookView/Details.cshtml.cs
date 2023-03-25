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
    public class DetailsModel : PageModel
    {
        private readonly BookRental.Data.BookRentalContext _context;

        public DetailsModel(BookRental.Data.BookRentalContext context)
        {
            _context = context;
        }

      public Books Books { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var books = await _context.Books.FirstOrDefaultAsync(m => m.Id == id);
            if (books == null)
            {
                return NotFound();
            }
            else 
            {
                Books = books;
            }
            return Page();
        }
    }
}
