using LibraryBookingSystem.Models;
using LibraryBookingSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace LibraryBookingSystem.Pages
{
    [Authorize(Roles = "Librarian, Administrator, Member")]
    public class ViewPublisherModel : PageModel
    {
        private readonly IBookRepository _bookRepository;

        public ViewPublisherModel(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        public Publisher ViewPublisher { get; set; }

        public IActionResult OnGet(int id)
        {
            ViewPublisher = _bookRepository.GetPublisherById(id);
            if (ViewPublisher == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}