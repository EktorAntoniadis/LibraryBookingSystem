using LibraryBookingSystem.Models;
using LibraryBookingSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryBookingSystem.Pages
{
    [Authorize(Roles = "Administrator")]
    public class EditUserModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public EditUserModel(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        }

        [BindProperty]
        public User EditUser { get; set; }

        public IEnumerable<Role> AllRoles { get; set; }

        public IActionResult OnGet(int id)
        {
            EditUser = _userRepository.GetById(id);
            AllRoles = _roleRepository.GetAllRoles();
            return Page();
        }

        public IActionResult OnPostUpdateUser()
        {
            AllRoles = _roleRepository.GetAllRoles();
            _userRepository.Update(EditUser);
            return RedirectToPage("/Users");
        }
    }
}
