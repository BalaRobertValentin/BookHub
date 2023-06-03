using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookRental.Models;
using BookRental.Data;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace BookRental.Pages.FavoriteItem
{
    public class FavoriteItemModel : PageModel
    {
        private readonly BookRentalContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public FavoriteItemModel(BookRentalContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<BookRental.Models.FavoriteItem> FavoriteItems { get; set; }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new Exception($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            FavoriteItems = await _context.FavoriteItems.Include(f => f.Book).Where(f => f.UserId == user.Id).ToListAsync();
        }
        public async Task<IActionResult> OnPostAddToFavoritesAsync(int bookId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var book = await _context.Books.FindAsync(bookId);
            if (book == null)
            {
                return NotFound();
            }

           
            var isBookAlreadyFavorite = await _context.FavoriteItems.AnyAsync(f => f.UserId == user.Id && f.Book.Id == bookId);
            if (isBookAlreadyFavorite)
            {
                return RedirectToPage();
            }

            var favoriteItem = new BookRental.Models.FavoriteItem
            {
                UserId = user.Id,
                Book = book
            };
            _context.FavoriteItems.Add(favoriteItem);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRemoveAsync(int id)
        {
            var favoriteItem = await _context.FavoriteItems.FindAsync(id);
            if (favoriteItem == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (favoriteItem.UserId != user.Id)
            {
                return Unauthorized();
            }

            _context.FavoriteItems.Remove(favoriteItem);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}