using LibraryBookingSystem.Models;

namespace LibraryBookingSystem.Repositories
{
    public interface IAuthorRepository
    {
        IEnumerable<Author> GetAll();
        Genre? GetById(int id);
        void Add(Author author);
        void Update(Author author);
        void Delete(int id);
    }
}
