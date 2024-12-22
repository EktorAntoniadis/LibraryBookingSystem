using Microsoft.EntityFrameworkCore;

namespace LibraryBookingSystem.Models
{
    public class LibraryManagementDbContext : DbContext
    {
        public LibraryManagementDbContext(DbContextOptions options) :
            base(options)
        {                
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RentedUserBook> RentedUserBooks { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
