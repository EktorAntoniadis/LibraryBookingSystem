using LibraryBookingSystem.Common;
using LibraryBookingSystem.Models;
using LibraryBookingSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;

namespace LibraryBookingSystem.Pages
{
    [Authorize(Roles = "Librarian, Administrator")]
    public class ViewUserModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public ViewUserModel(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public User User { get; set; }
        public List<string> Permissions { get; set; }

        public IActionResult OnGet(int id)
        {
            User = _userRepository.GetById(id);
            if (User == null)
            {
                return NotFound();
            }         
            return Page();
        }
    }
}