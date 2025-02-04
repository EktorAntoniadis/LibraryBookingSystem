using LibraryBookingSystem.Models;
using LibraryBookingSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryBookingSystem.Pages
{
    [Authorize(Roles = "Librarian, Administrator")]
    public class EditRentedUserBookModel : PageModel
    {
        private readonly IInventoryRepository _inventoryRepository;

        public EditRentedUserBookModel(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository ?? throw new ArgumentNullException(nameof(inventoryRepository));
        }

        [BindProperty]
        public RentedUserBook EditRentedUserBook { get; set; }
        public IActionResult OnGet(int id)
        {
            EditRentedUserBook = _inventoryRepository.GetRentedUserBook(id);
            return Page();
        }

        public IActionResult OnPostUpdateRentedUserBook()
        {
            var editRentedBook = new RentedUserBook
            {
                BookId = EditRentedUserBook.BookId,
                RentedUserBookId = EditRentedUserBook.RentedUserBookId,
                UserId = EditRentedUserBook.UserId,
                DueDate = EditRentedUserBook.DueDate,
                ReturnDate = EditRentedUserBook.ReturnDate,
                RentedDate = EditRentedUserBook.RentedDate,
                Status = EditRentedUserBook.Status,
            };
            _inventoryRepository.UpdateRentedBook(editRentedBook);
            return RedirectToPage("/RentedUserBooks");
        }
    }
}
