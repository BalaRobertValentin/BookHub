using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookRental.Models;
using System.Globalization;


namespace BookRental.Pages.Book
{
    public class IndexModel : PageModel
    {
        private readonly BookRental.Data.BookRentalContext _context;

        public IndexModel(BookRental.Data.BookRentalContext context)
        {
            _context = context;
        }

        public IList<Books> Books { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Sort { get; set; } = "priceasc";

        public async Task OnGetAsync()
        {
            var books = from x in _context.Books select x;

            if (!string.IsNullOrEmpty(SearchString))
            {
                books = books.Where(s => s.Title != null && s.Title.Contains(SearchString) || s.Author != null && s.Author.Contains(SearchString));
            }

            switch (Sort?.ToLower())
            {
                case "priceasc":
                    books = books.OrderBy(s => s.RentalPrice);
                    break;
                case "pricedesc":
                    books = books.OrderByDescending(s => s.RentalPrice);
                    break;
                case "newestbook":
                    books = books.OrderByDescending(s => s.Year);
                    break;
                default:
                    break;
            }

            Books = await books.ToListAsync();
        }

        public string GetShortDescription(string? description)
        {
            const int maxLength = 100;
            if (string.IsNullOrEmpty(description)) return string.Empty;
            return description.Length <= maxLength ? description : description.Substring(0, maxLength) + "...";
        }
    }
}
