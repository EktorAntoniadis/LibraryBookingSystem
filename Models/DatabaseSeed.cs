using Microsoft.AspNetCore.Identity;
using System.Numerics;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraryBookingSystem.Models
{
    public class DatabaseSeed
    {
        private readonly LibraryManagementDbContext _context;
        private PasswordHasher<User> _hasher;

        public DatabaseSeed(LibraryManagementDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _hasher = new PasswordHasher<User>();
        }

        public void Seed()
        {
            var permisionAddAbook = new Permission
            {
                PermissionName = "Add a book",
                Description = "Permission to add a book",

            };

            var permisionDeleteABook = new Permission
            {
                PermissionName = "Delete a book",
                Description = "Permission to Delete a book",

            };

            var permisionUpdateABook = new Permission
            {
                PermissionName = "Read a book",
                Description = "Permission to Read a book",

            };

            var permisionReadABook = new Permission
            {
                PermissionName = "Delete a book",
                Description = "Permission to aDelete a book",
            };

            var permisionReadBooks = new Permission
            {
                PermissionName = "Read books",
                Description = "Permission to Read books",

            };

            var permisionAddMember = new Permission
            {
                PermissionName = "Add Member",
                Description = "Permission to Add Member",

            };

            var permisionUpdateMember = new Permission
            {
                PermissionName = "Update Member",
                Description = "Permission to Update Member",

            };

            var permisionDeleteMember = new Permission
            {
                PermissionName = "Delete Member",
                Description = "Permission to Delete Member",

            };

            var permisionReadMember = new Permission
            {
                PermissionName = "Read Member",
                Description = "Permission to Read Member",

            };

            var permisionReadMembers = new Permission
            {
                PermissionName = "Read Members",
                Description = "Permission to Read Members",

            };

            if (!_context.Permissions.Any())
            {
                _context.Permissions.AddRange(permisionAddAbook, permisionDeleteABook, permisionUpdateABook, permisionReadABook, permisionReadBooks, permisionAddMember, permisionUpdateMember, permisionDeleteMember, permisionReadMember, permisionReadMembers);
                _context.SaveChanges();
            }

            var roleLibrarian = new Role
            {
                RoleName = "Librarian",
                Description = "This is the role of a librarian",
                Permissions = new List<Permission>()
                  {
                      permisionAddAbook,
                      permisionAddMember,
                      permisionUpdateABook,
                      permisionUpdateMember,
                      permisionReadMember,
                      permisionReadMembers,
                      permisionDeleteABook
                  }
            };

            var roleAdministrator = new Role
            {
                RoleName = "Administrator",
                Description = "This is the role of a adminstrator",
                Permissions = new List<Permission>()
                  {
                      permisionAddAbook,
                      permisionAddMember,
                      permisionDeleteMember,
                      permisionReadMember,
                      permisionReadMembers,

                  }
            };

            var roleMember = new Role
            {
                RoleName = "Administrator",
                Description = "This is the role of a member",
                Permissions = new List<Permission>()
                  {
                      permisionReadABook,
                      permisionReadBooks,
                      permisionReadMember
                  }
            };


            if (!_context.Roles.Any())
            {
                _context.Roles.AddRange(roleLibrarian, roleAdministrator, roleMember);
                _context.SaveChanges();
            }

            var memberUser = new User
            {
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@example.com",
                PhoneNumber = "1234567890",
                RegisteredDate = new DateOnly(2020, 5, 20),
                Username = "jdoe",
                Role = roleMember
            };

            memberUser.Password = _hasher.HashPassword(memberUser, "!memberUser!1234");

            var administratorUser = new User
            {
                FirstName = "Han",
                LastName = "Solo",
                Email = "Han.Solo@example.com",
                PhoneNumber = "9723014857",
                RegisteredDate = new DateOnly(1990, 3, 12),
                Username = "hsolo",
                Role = roleAdministrator
            };

            administratorUser.Password = _hasher.HashPassword(administratorUser, "!administratorUser!5678");

            var librarianUser = new User
            {
                FirstName = "Igor",
                LastName = "Karkaroff",
                Email = "Igor.Karkaroff@example.com",
                PhoneNumber = "3017307376",
                RegisteredDate = new DateOnly(1991, 3, 15),
                Username = "ikark",
                Role = roleLibrarian
            };
            librarianUser.Password = _hasher.HashPassword(librarianUser, "!librarianUser!9628");

            if (!_context.Users.Any())
            {
                _context.Users.AddRange(memberUser, administratorUser, librarianUser);
                _context.SaveChanges();
            }
        }
    }
}
