using LibraryBookingSystem.Models;
using LibraryBookingSystem.Repositories;
using LibraryBookingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryBookingSystem.Pages
{
    [Authorize(Roles = "Librarian, Administrator")]
    public class AddBookModel : PageModel
    {
        private readonly IBookRepository _bookRepository;

        public AddBookModel(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        [BindProperty]

        public Book NewBook { get; set; }

        [BindProperty]
        public string NewAuthors { get; set; }

        public IEnumerable<Publisher> Publishers { get; set; }

        public IEnumerable<Genre> Genres { get; set; }
        public IActionResult OnGet()
        {
            NewBook = new Book()
            {
                Genre = new Genre(),
                Publisher = new Publisher(),
                Authors = new List<Author>()
            };

            Publishers = _bookRepository.GetAllPublishers();
            Genres = _bookRepository.GetAllGenres();

            return Page();
        }

        public IActionResult OnPostAddNewBook()
        {
            NewBook.Authors = new List<Author>();

            var authors = NewAuthors.Split(',');
            foreach(var author in authors)
            {
                //TODO: Να βάλω λειτουργικότητα για Middle name
                var firstName = author.Split(' ')[0];
                var lastName = author.Split(' ')[1];
                var existingAuthor = _bookRepository.GetAuthorByName(firstName, lastName);

                if(existingAuthor != null)
                {
                    NewBook.Authors.Add(existingAuthor);
                }
                else
                {
                    var newAuthor = new Author()
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Books = new List<Book>()
                    };
                    NewBook.Authors.Add(newAuthor);
                }
            }

            NewBook.Summary = TextDecryptor.EncryptText(NewBook.Summary);
            _bookRepository.AddBook(NewBook);
            return RedirectToPage("/Books");
        }
    }
}
