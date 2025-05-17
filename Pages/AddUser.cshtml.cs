using LibraryBookingSystem.Models;
using LibraryBookingSystem.Repositories;
using LibraryBookingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryBookingSystem.Pages
{
    [Authorize(Roles = "Administrator")]
    public class AddUserModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly SimplePasswordValidator _validator;

        public AddUserModel(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _passwordHasher = new PasswordHasher<User>();
            _validator = new SimplePasswordValidator();
        }

        [BindProperty]
        public User NewUser { get; set; }
        public string ErrorMessage { get; set; }
        public List<string> PasswordErrors { get; set; } = new();
        public IEnumerable<Role> AllRoles { get; set; }

        public IActionResult OnGet()
        {
            NewUser = new User();
            AllRoles = _roleRepository.GetAllRoles();
            return Page();
        }

        public IActionResult OnPostAddNewUser()
        {
            var existingUser = _userRepository.GetByEmailOrUsername(NewUser);

            if (existingUser != null)
            {
                ErrorMessage = "There is already a user with that email or username. Please enter another email or username";
                return Page();
            }

            PasswordErrors = _validator.Validate(NewUser.Password);

            if (PasswordErrors.Any())
            {
                return Page();
            }
            var hashedPassword = _passwordHasher.HashPassword(NewUser, NewUser.Password);

            NewUser.Password = hashedPassword;
            NewUser.RegisteredDate = DateOnly.FromDateTime(DateTime.Now);

            _userRepository.Add(NewUser);
            return RedirectToPage("Users");
        }
    }
}
