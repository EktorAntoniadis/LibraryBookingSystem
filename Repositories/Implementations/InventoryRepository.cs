using LibraryBookingSystem.Models;

namespace LibraryBookingSystem.Repositories.Implementations
{
    public class InventoryRepository
    {
        private LibraryManagementDbContext _context;
        public InventoryRepository(LibraryManagementDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void Add(Inventory inventory)
        {
            _context.Inventories.Add(inventory);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var inventory = GetById(id);
            _context.Inventories.Remove(inventory);
            _context.SaveChanges();
        }

        public IEnumerable<Inventory> GetAll()
        {
            var inventorys = _context.Inventories.ToList();
            return inventorys;

        }

        public Inventory? GetById(int id)
        {
            var inventory = _context.Inventories.Find(id);
            return inventory;
        }

        public void Update(Inventory inventorys)
        {
            _context.Inventories.Update(inventorys);
            _context.SaveChanges();
        }
    }
}