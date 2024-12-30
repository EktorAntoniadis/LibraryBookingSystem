using LibraryBookingSystem.Models;

namespace LibraryBookingSystem.Repositories.Implementations
{
    public class InventoryRepository : IInventoryRepository
    {
        private LibraryManagementDbContext _context;
        public InventoryRepository(LibraryManagementDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddInventory(Inventory inventory)
        {
            throw new NotImplementedException();
        }

        public void AddRentedBook(RentedUserBook rentedBook)
        {
            throw new NotImplementedException();
        }

        public void DeleteInventory(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Inventory> GetAllInventories()
        {
            throw new NotImplementedException();
        }

        public Genre? GetInventoryById(int id)
        {
            throw new NotImplementedException();
        }

        public RentedUserBook GetRentedUserBook(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RentedUserBook> GetUserRentedBooks(int userId)
        {
            throw new NotImplementedException();
        }

        public void UpdateInventory(Inventory inventory)
        {
            throw new NotImplementedException();
        }

        public void UpdateRentedBook(RentedUserBook rentedBook)
        {
            throw new NotImplementedException();
        }
    }
}