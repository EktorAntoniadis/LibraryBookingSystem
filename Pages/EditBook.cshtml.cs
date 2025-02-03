using LibraryBookingSystem.Models;
using LibraryBookingSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryBookingSystem.Pages
{
    [Authorize(Roles = "Librarian, Administrator")]
    public class EditBookModel : PageModel
    {
        private readonly IBookRepository _bookRepository;

        public EditBookModel(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        [BindProperty]
        public Book EditBook { get; set; }

        [BindProperty]
        public string NewAuthors { get; set; }
        public IEnumerable<Publisher> Publishers { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public string ErrorMessage { get; set; }

        public IActionResult OnGet(int id)
        {
            EditBook = _bookRepository.GetBookById(id);
            if (EditBook == null)
            {
                ErrorMessage = "Book not found.";
                return RedirectToPage("/Books");
            }

            Publishers = _bookRepository.GetAllPublishers();
            Genres = _bookRepository.GetAllGenres();
            NewAuthors = string.Join(", ", EditBook.Authors.Select(a => $"{a.FirstName} {a.LastName}"));

            return Page();
        }

        public IActionResult OnPostEditBook()
        {
            try
            {
                EditBook.Authors = new List<Author>();
                _bookRepository.UpdateBook(EditBook);
                var authors = NewAuthors.Split(',');
                foreach (var author in authors)
                {
                    var names = author.Trim().Split(' ');
                    var firstName = names[0];
                    var lastName = names.Length > 1 ? names[1] : string.Empty;
                    var existingAuthor = _bookRepository.GetAuthorByName(firstName, lastName);

                    if (existingAuthor != null)
                    {
                        continue;
                    }
                    else
                    {
                        var newAuthor = new Author()
                        {
                            FirstName = firstName,
                            LastName = lastName,
                            Books = new List<Book>()
                        };
                        EditBook.Authors.Add(newAuthor);
                    }
                }

                _bookRepository.UpdateBook(EditBook);
                return RedirectToPage("/Books");
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred: {ex.Message}";
                return Page();
            }
        }
    }
}
