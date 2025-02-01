using LibraryBookingSystem.Common;
using LibraryBookingSystem.Models;
using LibraryBookingSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryBookingSystem.Pages
{
    [Authorize(Roles = "Librarian, Administrator, Member")]
    public class ViewBookModel : PageModel
    {
        private readonly IBookRepository _bookRepository;

        public ViewBookModel(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        [BindProperty]
        public Book ViewBook { get; set; }

        public IActionResult OnGet(int id)
        {
            ViewBook = _bookRepository.GetBookById(id);
            return Page();
        }
    }
}