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
        public Author EditAuthor { get; set; }

        public List<Author> Authors { get; set; } = new List<Author>();

        public void OnGet()
        {
            Authors = _repository.GetAllAuthors().ToList();
        }

        public IActionResult OnPostUpdate(int id)
        {
            var editAuthor = _repository.GetAuthorById(id);
            if (editAuthor == null)
            {
                return NotFound();
            }
            EditAuthor = editAuthor;
            Authors = _repository.GetAllAuthors().ToList();
            return Page();
        }

        public IActionResult OnPostEditAuthor()
        {
            if (!string.IsNullOrWhiteSpace(EditAuthor.FirstName) && !string.IsNullOrEmpty(EditAuthor.LastName))
            {
                _repository.UpdateAuthor(EditAuthor);
            }

            return RedirectToPage("/Authors");
        }     
    }
}