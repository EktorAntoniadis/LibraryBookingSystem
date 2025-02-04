using LibraryBookingSystem.Models;
using LibraryBookingSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryBookingSystem.Pages
{
    [Authorize(Roles = "Librarian, Administrator")]
    public class DeleteBookModel : PageModel
    {
        private readonly IBookRepository _bookRepository;

        public DeleteBookModel(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        [BindProperty]
        public Book DeleteBook { get; set; }

        public IActionResult OnGet(int id)
        {
            DeleteBook = _bookRepository.GetBookById(id);
            if (DeleteBook == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            _bookRepository.DeleteBook(DeleteBook.BookId);
            return RedirectToPage("/Books");
        }
    }
}