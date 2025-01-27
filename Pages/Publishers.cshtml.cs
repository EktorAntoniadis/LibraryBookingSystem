using LibraryBookingSystem.Common;
using LibraryBookingSystem.Models;
using LibraryBookingSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace LibraryBookingSystem.Pages
{
    public class PublishersModel : PageModel
    {
        private readonly IBookRepository _bookRepository;

        public PublishersModel(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        public PaginatedList<Publisher> PaginatedPublishers { get; set; }

        [FromQuery]
        public string? Name { get; set; }

        [FromQuery]
        public string? Phone { get; set; }

        [FromQuery]
        public string? Address { get; set; }

        [FromQuery]
        public string? City { get; set; }

        [FromQuery]
        public string? Country { get; set; }

        [FromQuery]
        public string? SortDirection { get; set; }

        [FromQuery]
        public string? SortColumn { get; set; }

        [FromQuery]
        public int PageIndex { get; set; } = 1;

        public IActionResult OnGet()
        {
            PaginatedPublishers = _bookRepository.GetPublishers(
                PageIndex,
                10,
                Name,
                Phone,
                Address,
                City,
                Country,
                SortColumn,
                SortDirection
            );
            return Page();
        }       
    }
}
