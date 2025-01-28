using LibraryBookingSystem.Common;
using LibraryBookingSystem.Models;
using LibraryBookingSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryBookingSystem.Pages
{
    public class EditAuthorModel : PageModel
    {
        private readonly IBookRepository _bookRepository;

        public EditAuthorModel(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        [BindProperty]
        public Author EditAuthor { get; set; }

        public IActionResult OnGet(int id)
        {
            EditAuthor = _bookRepository.GetAuthorById(id);
            if (EditAuthor == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPostEditAuthor()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _bookRepository.UpdateAuthor(EditAuthor);
            return RedirectToPage("/Authors");
        }
    }
}
