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

        [BindProperty]
        public Genre EditGenre { get; set; }

        public List<Genre> Genres { get; set; } = new List<Genre>();

        public void OnGet()
        {
            Genres = _repository.GetAllGenres().ToList();
        }

        public IActionResult OnPostUpdate(int id)
        {
            var editGenre = _repository.GetGenreById(id);
            if(editGenre == null)
            {
                return NotFound();
            }

            Genres = _repository.GetAllGenres().ToList();
            EditGenre = editGenre;
            return Page();
        }

        public IActionResult OnPostEditGenre()
        {
            _repository.UpdateGenre(EditGenre);
            EditGenre = null;
            return RedirectToPage("/Genres");
        }
        public IActionResult OnPostAddGenre()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _repository.AddGenre(NewGenre);
            return RedirectToPage();
        }
    }
}