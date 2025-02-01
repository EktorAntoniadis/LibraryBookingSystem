using LibraryBookingSystem.Common;
using LibraryBookingSystem.Models;
using LibraryBookingSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace LibraryBookingSystem.Pages
{
    [Authorize(Roles = "Librarian, Administrator, Member")]
    public class BooksModel : PageModel
    {
        private readonly IBookRepository _bookRepository;

        public BooksModel(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        public IEnumerable<Genre> Genres { get; set; }
        public PaginatedList<Book> Books { get; set; }

        [FromQuery]
        public string? Title { get; set; }

        [FromQuery]
        public string? ISBN { get; set; }

        [FromQuery]
        public string? Genre { get; set; }

        [FromQuery]
        public string? Author { get; set; }

        [FromQuery]
        public string? SortDirection { get; set; }

        [FromQuery]
        public string? SortColumn { get; set; }

        [FromQuery]
        public int PageIndex { get; set; } = 1;

        public IActionResult OnGet()
        {
            Genres = _bookRepository.GetAllGenres();
            Books = _bookRepository.GetBooks(PageIndex, 10, Title, Genre, Author, ISBN, SortColumn, SortDirection);
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