using BookRental.Data;
using BookRental.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookRental.Pages.BookView
{
    public class ReserveModel : PageModel
    {
        private readonly BookRentalContext _context;

        public ReserveModel(BookRental.Data.BookRentalContext context)
        {
            _context = context;
        }


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
        [BindProperty]
        public Client Client { get; set; } = default!;
        public Books Books { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Client == null || Client == null)
            {
                return RedirectToPage("/ContactView/Unconfirmed");
            }

            _context.Client.Add(Client);
            await _context.SaveChangesAsync();

            return RedirectToPage("/ContactView/Confirmed");
        }
    }
}
