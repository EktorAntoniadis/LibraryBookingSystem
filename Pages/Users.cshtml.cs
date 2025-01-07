using LibraryBookingSystem.Models;
using LibraryBookingSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryBookingSystem.Pages
{
    public class UsersModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public UsersModel(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public IEnumerable<User> Users { get; set; } = new List<User>();

        public IActionResult OnGet()
        {
            Users = _userRepository.GetAll();
            return Page();
        }
    }
}