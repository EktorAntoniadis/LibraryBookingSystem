using LibraryBookingSystem.Common;
using LibraryBookingSystem.Models;
using LibraryBookingSystem.Repositories;
using LibraryBookingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryBookingSystem.Pages
{
    [Authorize(Roles = "Librarian, Administrator, Member")]
    public class ViewBookModel : PageModel
    {
        private readonly IBookRepository _bookRepository;
        private readonly IInventoryRepository _inventoryRepository;

        public ViewBookModel(IBookRepository bookRepository, IInventoryRepository inventoryRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _inventoryRepository = inventoryRepository ?? throw new ArgumentNullException(nameof(inventoryRepository));
        }

        [BindProperty]
        public Book ViewBook { get; set; }

        public string ErrorMessage { get; set; }

        public IActionResult OnGet(int id)
        {
            ViewBook = _bookRepository.GetBookById(id);
            ViewBook.Summary = TextDecryptor.DecryptText(ViewBook.Summary);
            return Page();
        }

        public IActionResult OnPostCheckOut(int id)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToPage("/Error");
            }

            var bookInventory = _inventoryRepository.GetInventoryByBookId(ViewBook.BookId);

            if (bookInventory.AvailableNumberOfCopies == 0)
            {
                ErrorMessage = "There are no available copies to borrow. Please select another book";
                ViewBook = _bookRepository.GetBookById(id);
                return Page();
            }

            if (_inventoryRepository.IsBookRented((int)userId, ViewBook.BookId))
            {
                ErrorMessage = "You have already rented this book. Please select another book";
                ViewBook = _bookRepository.GetBookById(id);
                return Page();
            }

            bookInventory.AvailableNumberOfCopies = bookInventory.AvailableNumberOfCopies-1;

            _inventoryRepository.UpdateInventory(bookInventory);

            var rentedUserBook = new RentedUserBook
            {
                BookId = ViewBook.BookId,
                UserId = (int)userId,
                RentedDate = DateOnly.FromDateTime(DateTime.Now),
                DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(7)),
                Status = "Checked Out"
            };

            _inventoryRepository.AddRentedBook(rentedUserBook);
            return RedirectToPage("/RentedUserBooks");
        }
    }
}