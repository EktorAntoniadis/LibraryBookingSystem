using LibraryBookingSystem.Models;

namespace LibraryBookingSystem.Repositories
{
    public interface IPermissionRepository
    {
        IEnumerable<Permission> GetAll();
        Permission? GetById(int id);
        void Add(Permission permission);
        void Update(Permission permission);
        void Delete(int id);
    }
}
