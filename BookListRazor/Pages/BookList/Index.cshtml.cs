using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Book> Books { get; set; }

        [TempData]
        public string Message { get; set; }

        public async Task OnGet()
        {
            Books = await _db.Book.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            // grab the book to delete
            var book = await _db.Book.FindAsync(id);

            // check for null
            if (book == null)
            {
                return NotFound();
            }

            // remove from the db
            _db.Book.Remove(book);
            await _db.SaveChangesAsync();

            // display message 
            Message = "Book deleted successfully!";

            // refresh the page
            return RedirectToPage("Index");
        }
    }
}