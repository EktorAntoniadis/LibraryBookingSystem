using LibraryBookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryBookingSystem.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private LibraryManagementDbContext _context;
        public UserRepository(LibraryManagementDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = GetById(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            var user = _context.Users.ToList();
            return user;
        }

        public User? GetById(int id)
        {
            var user = _context.Users.Find(id);
            return user;
        }

        public User? GetByUserName(string userName)
        {
            var user = _context.Users
                .Include(x=>x.Role)
                .Where(x => x.Username == userName).FirstOrDefault();
            return user;
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
