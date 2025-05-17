using LibraryBookingSystem.Models;
using LibraryBookingSystem.Repositories;
using LibraryBookingSystem.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryBookingSystem.Pages
{
    public class ChangePasswordModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private PasswordHasher<User> _hasher;
        private readonly SimplePasswordValidator _validator;
        public ChangePasswordModel(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _hasher = new PasswordHasher<User>();
            _validator = new SimplePasswordValidator();
        }

        [BindProperty]
        public User LoggedInUser { get; set; }

        [BindProperty]
        public string CurrentPassword { get; set; }

        [BindProperty]
        public string NewPassword { get; set; }

        [BindProperty]
        public string ConfirmPassword { get; set; }

        public string StatusMessage { get; set; } = "";
        public List<string> ValidationErrors { get; set; } = new();

        public IActionResult OnGet()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            LoggedInUser = _userRepository.GetById(userId!.Value);

            CurrentPassword = LoggedInUser.Password;

            return Page();
        }

        public IActionResult OnPostChangeOldPassword()
        {
            if (string.IsNullOrEmpty(NewPassword))
            {
                ValidationErrors.Add("New Password is required.");
                return Page();
            }

            if (string.IsNullOrEmpty(ConfirmPassword))
            {
                ValidationErrors.Add("Confirm Password is required.");
                return Page();
            }

            if (NewPassword != ConfirmPassword)
            {
                ValidationErrors.Add("New password and confirmation do not match.");
                return Page();
            }

            ValidationErrors = _validator.Validate(NewPassword);

            if (ValidationErrors.Count > 0)
            {
                return Page();
            }

            var userId = HttpContext.Session.GetInt32("UserId");

            LoggedInUser = _userRepository.GetById(userId!.Value);

            LoggedInUser.Password = _hasher.HashPassword(LoggedInUser, NewPassword);

            _userRepository.Update(LoggedInUser);

            return RedirectToPage("/Profile");
        }
    }
}
