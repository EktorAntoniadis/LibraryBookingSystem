using LibraryBookingSystem.Models;
using LibraryBookingSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryBookingSystem.Pages
{
    [Authorize(Roles = "Librarian, Administrator, Member")]
    public class ProfileModel : PageModel
    {
        private IUserRepository _userRepository;

        public ProfileModel(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [BindProperty]
        public User LoggedInUser { get; set; }

        public IActionResult OnGet()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            LoggedInUser = _userRepository.GetById(userId!.Value);

            return Page();
        }
    }
}
