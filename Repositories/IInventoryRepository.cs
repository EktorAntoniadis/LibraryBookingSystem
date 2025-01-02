using LibraryBookingSystem.Models;

namespace LibraryBookingSystem.Repositories
{
    public interface IInventoryRepository
    {
        IEnumerable<Inventory> GetAllInventories();
        Inventory? GetInventoryById(int id);
        void AddInventory(Inventory inventory);
        void UpdateInventory(Inventory inventory);
        void DeleteInventory(int id);
        void AddRentedBook(RentedUserBook rentedBook);
        void UpdateRentedBook(RentedUserBook rentedBook);
        RentedUserBook GetRentedUserBook(int id);
        IEnumerable<RentedUserBook> GetUserRentedBooks(int userId);
    }
}
