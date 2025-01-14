using LibraryBookingSystem.Models;
using LibraryBookingSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryBookingSystem.Pages
{
    public class AuthorsModel : PageModel
    {
        private readonly IBookRepository _repository;

        public AuthorsModel(IBookRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [BindProperty]
        public IEnumerable<Author> Authors { get; set; }

        public IActionResult OnGet()
        {
            Authors = _repository.GetAllAuthors();
            return Page();
        }

        public IActionResult OnPostDelete(int id)
        {
            var author = _repository.GetAuthorById(id);
            if (author == null)
            {
                return NotFound();
            }

            _repository.DeleteAuthor(id);
            return RedirectToPage();
        }
    }
}
