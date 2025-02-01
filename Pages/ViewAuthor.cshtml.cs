using LibraryBookingSystem.Common;
using LibraryBookingSystem.Models;
using LibraryBookingSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryBookingSystem.Pages
{
    [Authorize(Roles = "Librarian, Administrator, Member")]
    public class ViewAuthorModel : PageModel
    {
        private readonly IBookRepository _bookRepository;

        public ViewAuthorModel(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        public Author Author { get; set; }

        public IActionResult OnGet(int id)
        {
            Author = _bookRepository.GetAuthorById(id);
            if (Author == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}