using LibraryBookingSystem.Models;

namespace LibraryBookingSystem.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User? GetById(int id);
        User? GetByUserName(string userName);

        User? GetByEmailOrUsername(User user);
        void Add(User user);
        void Update(User user);
        void Delete(int id);
    }
}
