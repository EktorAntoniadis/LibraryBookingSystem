using LibraryBookingSystem.Models;
using LibraryBookingSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryBookingSystem.Pages
{
    [Authorize(Roles = "Administrator")]
    public class DeleteUserModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserModel(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [BindProperty]
        public User DeleteUser { get; set; }

        public IActionResult OnGet(int id)
        {
            DeleteUser = _userRepository.GetById(id);

            return Page();
        }

        public IActionResult OnPost() 
        {
            _userRepository.Delete(DeleteUser.UserId);
            return RedirectToPage("/Users");
        }
    }
}
