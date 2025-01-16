using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LibraryBookingSystem.Models;
using LibraryBookingSystem.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace LibraryBookingSystem.Pages
{
    public class PublishersModel : PageModel
    {
        private readonly IBookRepository _repository;

        public PublishersModel(IBookRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [BindProperty]
        public Publisher EditPublisher { get; set; }
     
        public List<Publisher> Publishers { get; set; } = new List<Publisher>();

        public void OnGet()
        {
            Publishers = _repository.GetAllPublishers().ToList();
        }

        public IActionResult OnPostUpdate(int id)
        {
            var editPublisher = _repository.GetPublisherById(id);
            if (editPublisher == null)
            {
                return NotFound();
            }
            EditPublisher = editPublisher;
            Publishers = _repository.GetAllPublishers().ToList();
            return Page();
        }

        public IActionResult OnPostEditPublisher()
        {
            if (!string.IsNullOrWhiteSpace(EditPublisher.Name))
            {
                _repository.UpdatePublisher(EditPublisher);
            }

            return RedirectToPage("/Publishers");
        }

       
    }
}
