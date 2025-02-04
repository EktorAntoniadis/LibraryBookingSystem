using LibraryBookingSystem.Common;
using LibraryBookingSystem.Models;

namespace LibraryBookingSystem.Repositories
{
    public interface IInventoryRepository
    {
        PaginatedList<Inventory> GetInventory(
            int pageIndex,
            int pageSize,
            int? availableNumberOfCopies,
            string? bookTitle,
            string? sortColumn = "Title",
            string? sortDirection = "asc");
        Inventory? GetInventoryById(int id);
        Inventory? GetInventoryByBookId(int bookId);
        void AddInventory(Inventory inventory);
        void UpdateInventory(Inventory inventory);
        void DeleteInventory(int id);
        void AddRentedBook(RentedUserBook rentedBook);
        void UpdateRentedBook(RentedUserBook rentedBook);
        RentedUserBook GetRentedUserBook(int id);
        IEnumerable<RentedUserBook> GetUserRentedBooks(int userId);
        IEnumerable<RentedUserBook> GetAllMembersRentedBooks();
        IEnumerable<RentedUserBook> GetOverdueRentedBooks(int userId);
        bool IsBookRented(int userId, int bookId);
    }
}
