using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookRental.Data;
using BookRental.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookRental.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly BookRentalContext _context; // Add a field for the BookRentalContext

        public IndexModel(ILogger<IndexModel> logger, BookRentalContext context) // Inject the BookRentalContext into the constructor
        {
            _logger = logger;
            _context = context;
        }

        public IList<Books> Books { get; set; } = default!;

        public async Task OnGetAsync() // Change the method to be async
        {
            Books = await _context.Books.ToListAsync(); // Retrieve the data using Entity Framework
        }
    }
}