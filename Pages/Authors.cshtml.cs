using LibraryBookingSystem.Common;
using LibraryBookingSystem.Models;
using LibraryBookingSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace LibraryBookingSystem.Pages
{
    [Authorize(Roles = "Librarian, Administrator, Member")]
    public class AuthorsModel : PageModel
    {
        private readonly IBookRepository _bookRepository;

        public AuthorsModel(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        [BindProperty]
        public Author EditAuthor { get; set; }
        public PaginatedList<Author> Authors { get; set; }

        [FromQuery]
        public string? FirstName { get; set; }

        [FromQuery]
        public string? LastName { get; set; }

        [FromQuery]
        public string? SortDirection { get; set; }

        [FromQuery]
        public string? SortColumn { get; set; }

        [FromQuery]
        public int PageIndex { get; set; } = 1;

        public IActionResult OnGet()
        {
            Authors = _bookRepository.GetAuthors(PageIndex, 10, FirstName, LastName, SortColumn, SortDirection);
            return Page();
        }

        public IActionResult OnPostUpdate(int id)
        {
            var editAuthor = _bookRepository.GetAuthorById(id);
            if (editAuthor == null)
            {
                return NotFound();
            }
            EditAuthor = editAuthor;
            Authors = _bookRepository.GetAuthors(PageIndex, 10, FirstName, LastName, SortColumn, SortDirection);
            return Page();
        }

        public IActionResult OnPostEditAuthor()
        {
            if (!string.IsNullOrWhiteSpace(EditAuthor.FirstName) && !string.IsNullOrEmpty(EditAuthor.LastName))
            {
                _bookRepository.UpdateAuthor(EditAuthor);
            }

            return RedirectToPage("/Authors");
        }
    }
}
