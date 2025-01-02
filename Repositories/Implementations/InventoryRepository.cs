using LibraryBookingSystem.Models;

namespace LibraryBookingSystem.Repositories.Implementations
{
    public class InventoryRepository : IInventoryRepository
    {
        private LibraryManagementDbContext _context;
        private Inventory inventory;

        public InventoryRepository(LibraryManagementDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddInventory(Inventory inventory)
        {
            _context.Inventories.Add(inventory);
            _context.SaveChanges();
        }

        public void AddRentedBook(RentedUserBook rentedBook)
        {
            throw new NotImplementedException();
        }

        public void DeleteInventory(int id)
        {
            //Υλοποίηση αυτής της μεθόδου
            var inventory = GetInventoryById(id);
            _context.Inventories.Remove(inventory);
            _context.SaveChanges();
        }

        public IEnumerable<Inventory> GetAllInventories()
        {
            var inventories = _context.Inventories.ToList();
            return inventories;
        }

        public Inventory? GetInventoryById(int id)
        {
            var inventory = _context.Inventories.Find(id);
            return inventory;
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
            _context.Inventories.Update(inventory);
            _context.SaveChanges();
        }

        public void UpdateRentedBook(RentedUserBook rentedBook)
        {
            throw new NotImplementedException();
        }
    }
}