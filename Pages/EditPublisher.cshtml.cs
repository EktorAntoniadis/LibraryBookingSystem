using LibraryBookingSystem.Models;
using LibraryBookingSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryBookingSystem.Pages
{
    public class EditPublisherModel : PageModel
    {
        private readonly IBookRepository _bookRepository;

        public EditPublisherModel(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        [BindProperty]
        public Publisher EditPublisher { get; set; }

        public string ErrorMessage { get; set; }

        public IActionResult OnGet(int id)
        {
            EditPublisher = _bookRepository.GetPublisherById(id);

            if (EditPublisher == null)
            {
                ErrorMessage = "Publisher not found.";
                return RedirectToPage("/Publishers");
            }

            return Page();
        }

        public IActionResult OnPostEditPublisher()
        {          
            try
            {
                _bookRepository.UpdatePublisher(EditPublisher);
                return RedirectToPage("/Publishers");
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred: {ex.Message}";
                return Page();
            }
        }
    }
}
