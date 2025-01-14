using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LibraryBookingSystem.Models;
using LibraryBookingSystem.Repositories;

namespace LibraryBookingSystem.Pages
{
    public class GenreModel : PageModel
    {
        private readonly IBookRepository _repository;

        public GenreModel(IBookRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [BindProperty]
        public Genre NewGenre { get; set; } = new Genre();

        public List<Genre> Genres { get; set; } = new List<Genre>();

        public void OnGet()
        {
            Genres = _repository.GetAllGenres().ToList();
        }
        //                   Post απο τη φόρμα - Ονομα του Handler  
        public IActionResult OnPostAddGenre()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _repository.AddGenre(NewGenre);
            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int id)
        {
            var genre = _repository.GetGenreById(id);
            if (genre == null)
            {
                return NotFound();
            }

            _repository.DeleteGenre(id);
            return RedirectToPage();
        }
    }
}