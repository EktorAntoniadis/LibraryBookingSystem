using LibraryBookingSystem.Models;

namespace LibraryBookingSystem.Repositories
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAllBooks();
        Book? GetBookById(int id);
        void Add(Book book);
        void Update(Book book);
        void DeleteBook(int id);
        IEnumerable<Genre> GetAllGenres();
        Genre? GetGenreById(int id);
        void Add(Genre genre);
        void Update(Genre genre);
        void DeleteGenre(int id);
        IEnumerable<Author> GetAllAuthors();
        Author? GetAuthorById(int id);
        void Add(Author author);
        void Update(Author author);
        void DeleteAuthor(int id);
    }
}
