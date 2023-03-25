using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookRental.Data;
using BookRental.Models;

namespace BookRental.Pages.ContactView
{
    public class ContactModel : PageModel
    {
        private readonly BookRental.Data.BookRentalContext _context;

        public ContactModel(BookRental.Data.BookRentalContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Contact Contact { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Contact == null || Contact == null)
            {
				return RedirectToPage("/ContactView/Unconfirmed");
			}

            _context.Contact.Add(Contact);
            await _context.SaveChangesAsync();

            return  RedirectToPage("/ContactView/Confirmed");
        }
    }
}
