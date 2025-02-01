using LibraryBookingSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LibraryBookingSystem.Repositories.Implementations
{
    public class RoleRepository : IRoleRepository
    {
        private LibraryManagementDbContext _context;
        public RoleRepository(LibraryManagementDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
        }

        public void Add(Permission permission)
        {
            _context.Add(permission);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var role = GetRoleById(id);
            _context.Roles.Remove(role);
            _context.SaveChanges();
        }

        public void DeletePermission(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteRole(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetAll()
        {
            var role = _context.Roles.ToList();
            return role;
        }

        public IEnumerable<Permission> GetAllPermissions()
        {
            var permissions = _context.Permissions.Include(x => x.Roles).ToList();
            return permissions;
        }

        public IEnumerable<Role> GetAllRoles()
        {
            var roles = _context.Roles.ToList();
            return roles;
        }

        public Permission? GetPermissionById(int id)
        {
            var permission = _context.Permissions
                .Include(x => x.Roles)
                .FirstOrDefault(x => x.PermissionId == id);

            return permission;
        }

        public Role? GetRoleById(int id)
        {
            var role = _context.Roles
                .Include(x => x.Permissions)
                .FirstOrDefault(x => x.RoleId == id);
            return role;
        }

        public void Update(Role role)
        {
            _context.Roles.Update(role);
            _context.SaveChanges();
        }

        public void Update(Permission permission)
        {
            _context.Permissions.Update(permission);
            _context.SaveChanges();
        }
    }
}