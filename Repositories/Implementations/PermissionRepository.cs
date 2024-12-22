using LibraryBookingSystem.Models;

namespace LibraryBookingSystem.Repositories.Implementations
{
    public class PermissionRepository : IPermissionRepository
    {
        private LibraryManagementDbContext _context;
        public PermissionRepository(LibraryManagementDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void Add(Permission permission)
        {
            _context.Permissions.Add(permission);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var permission = GetById(id);
            _context.Permissions.Remove(permission);
            _context.SaveChanges();
        }

        public IEnumerable<Permission> GetAll()
        {
            var permissions = _context.Permissions.ToList();
            return permissions;
        }

        public Permission? GetById(int id)
        {
            var permission = _context.Permissions.Find(id);
            return permission;
        }

        public void Update(Permission permission)
        {
            _context.Permissions.Update(permission);
            _context.SaveChanges();
        }
    }
}
