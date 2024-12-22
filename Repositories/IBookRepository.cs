using LibraryBookingSystem.Models;

namespace LibraryBookingSystem.Repositories
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll();
        Genre? GetById(int id);
        void Add(Book book);
        void Update(Book book);
        void Delete(int id);
    }
}
