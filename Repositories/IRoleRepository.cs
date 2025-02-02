using LibraryBookingSystem.Models;

namespace LibraryBookingSystem.Repositories
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetAllRoles();
        Role? GetRoleById(int id);

        Role? GetRoleByName(string name);
        void Add(Role role);
        void Update(Role role);
        void DeleteRole(int id);
        IEnumerable<Permission> GetAllPermissions();
        Permission? GetPermissionById(int id);
        void Add(Permission permission);
        void Update(Permission permission);
        void DeletePermission(int id);
    }
}
