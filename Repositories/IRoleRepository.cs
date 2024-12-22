using LibraryBookingSystem.Models;

namespace LibraryBookingSystem.Repositories
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetAll();
        Role? GetById(int id);

        void Add(Role role);
        void Update(Role role);
        void Delete(int id);
    }
}
