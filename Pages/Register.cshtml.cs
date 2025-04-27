using LibraryBookingSystem.Models;
using LibraryBookingSystem.Repositories;
using LibraryBookingSystem.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryBookingSystem.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private PasswordHasher<User> _hasher;
        private readonly SimplePasswordValidator _validator;

        public RegisterModel(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _hasher = new PasswordHasher<User>();
            _validator = new SimplePasswordValidator();

        }

        [BindProperty]
        public User NewUser { get; set; }

        public string ErrorMessage { get; set; }

        public List<string> PasswordErrors { get; set; } = new();
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            ModelState.Remove("NewUser.Role");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            PasswordErrors = _validator.Validate(NewUser.Password);

            if (PasswordErrors.Any())
            {
                return Page();
            }
            else
            {
                var roleOfMember = _roleRepository.GetRoleByName("Member");
                if (roleOfMember != null)
                {
                    NewUser.RoleId = roleOfMember.RoleId;
                }

                var existingUser = _userRepository.GetByEmailOrUsername(NewUser);

                if (existingUser != null)
                {
                    ErrorMessage = "There is already a user with that email or username. Please enter another email or username";
                    return Page();
                }

                NewUser.RegisteredDate = DateOnly.FromDateTime(DateTime.Now);
                NewUser.Password = _hasher.HashPassword(NewUser, NewUser.Password);
                _userRepository.Add(NewUser);
                return RedirectToPage("/Login");
            }
        }
    }
}
