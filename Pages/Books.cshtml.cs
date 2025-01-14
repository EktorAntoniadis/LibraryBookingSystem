using LibraryBookingSystem.Models;
using LibraryBookingSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace LibraryBookingSystem.Pages
{
    public class BooksModel : PageModel
    {
        private readonly IBookRepository _bookRepository;

        public BooksModel(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        public IEnumerable<Book> Books { get; set; } = new List<Book>();

        public IActionResult OnGet()
        {
            Books = _bookRepository.GetAllBooks();
            return Page();
        }

        public IActionResult OnPostDelete(int id)
        {
            var book = _bookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            _bookRepository.DeleteBook(id);
            return RedirectToPage();
        }
    }
}