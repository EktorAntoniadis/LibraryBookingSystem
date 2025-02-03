using LibraryBookingSystem.Models;
using LibraryBookingSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryBookingSystem.Pages
{
    public class RentedUserBooksModel : PageModel
    {
        private readonly IInventoryRepository _inventoryRepository;

        public RentedUserBooksModel(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository ?? throw new ArgumentNullException(nameof(inventoryRepository));
        }

        public IEnumerable<RentedUserBook> RentedUserBooks { get; set; }
        public IActionResult OnGet()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            string? userRole = HttpContext.Session.GetString("UserRole");
            if (userId == null || string.IsNullOrWhiteSpace(userRole))
            {

                return RedirectToPage("/Error");
            }

            if (userRole == "Member")
            {
                RentedUserBooks = _inventoryRepository.GetUserRentedBooks((int)userId);
            }
            else
            {
                RentedUserBooks = _inventoryRepository.GetAllMembersRentedBooks();
            }

            return Page();
        }
    }
}
