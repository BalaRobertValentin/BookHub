using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookRental.Models;
using System.Globalization;
using BookRental.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace BookRental.Pages.Book
{


    public class IndexModel : PageModel

    {
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }

        private readonly BookRental.Data.BookRentalContext _context;
        private readonly CartService _cartService;

        public IndexModel(BookRental.Data.BookRentalContext context, CartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }

        public IList<Books> Books { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Sort { get; set; } = "priceasc";

        public async Task OnGetAsync(int currentPage = 1)
        {
            CurrentPage = currentPage;
            var books = from x in _context.Books select x;

            if (!string.IsNullOrEmpty(SearchString))
            {
                books = books.Where(s => s.Title != null && s.Title.Contains(SearchString) || s.Author != null && s.Author.Contains(SearchString));
            }

            switch (Sort?.ToLower())
            {
                case "priceasc":
                    books = books.OrderBy(s => s.Price);
                    break;
                case "pricedesc":
                    books = books.OrderByDescending(s => s.Price);
                    break;
                case "newestbook":
                    books = books.OrderByDescending(s => s.Year);
                    break;
                default:
                    break;
            }
            // Calculate the total number of pages.
            var count = await books.CountAsync();
            var pageSize = 9;  // The number of books on each page.
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            // Retrieve the books for the current page.
            Books = await books
                .Skip((CurrentPage - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

        }
        public async Task<IActionResult> OnPostAddToCartAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _cartService.AddBook(book, 1);
            return RedirectToPage();
        }

        public string GetShortDescription(string? description)
        {
            const int maxLength = 100;
            if (string.IsNullOrEmpty(description)) return string.Empty;
            return description.Length <= maxLength ? description : description.Substring(0, maxLength) + "...";
        }
    }
}
