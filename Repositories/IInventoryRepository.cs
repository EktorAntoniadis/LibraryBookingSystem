using LibraryBookingSystem.Models;

namespace LibraryBookingSystem.Repositories
{
    public interface IInventoryRepository
    {
        IEnumerable<Inventory> GetAll();
        Genre? GetById(int id);
        void Add(Inventory inventory);
        void Update(Inventory inventory);
        void Delete(int id);
    }
}
