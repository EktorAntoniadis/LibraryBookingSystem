using Microsoft.AspNetCore.Mvc.RazorPages;
using LibraryBookingSystem.Models;
using System.Collections.Generic;
using LibraryBookingSystem.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBookingSystem.Pages
{
    public class PublishersModel : PageModel
    {
        private readonly IBookRepository _bookRepository;

        public PublishersModel(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }
        public IEnumerable<Publisher> Publishers { get; set; } = new List<Publisher>(); 

        public IActionResult OnGet()
        {
            Publishers = _bookRepository.GetAllPublishers();
            return Page();
        }
    }
}