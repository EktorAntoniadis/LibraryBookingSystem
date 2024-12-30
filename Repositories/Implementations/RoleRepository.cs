using LibraryBookingSystem.Models;
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
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            var role = GetById(id);
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
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public Role? GetById(int id)
        {
            var role = _context.Roles.Find(id);
            return role;
        }

        public Permission? GetPermissionById(int id)
        {
            throw new NotImplementedException();
        }

        public Role? GetRoleById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Role role)
        {
            _context.Roles.Update(role);
            _context.SaveChanges();
        }

        public void Update(Permission permission)
        {
            throw new NotImplementedException();
        }
    }
}