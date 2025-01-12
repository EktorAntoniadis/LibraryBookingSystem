using LibraryBookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryBookingSystem.Repositories.Implementations
{
    public class BookRepository: IBookRepository
    {
        private LibraryManagementDbContext _context;
        public BookRepository(LibraryManagementDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void Add(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void Add(Genre genre)
        {
            _context.Genres.Add(genre);
            _context.SaveChanges();
        }

        public void Add(Author author)
        {
            _context.Authors.Add(author);
            _context.SaveChanges();
        }

        public void DeleteAuthor(int id)
        {
            var author = GetAuthorById(id);
            _context.Authors.Remove(author);
            _context.SaveChanges();
        }

        public void DeleteBook(int id)
        {
            var book = GetBookById(id);
            _context.Books.Remove(book);
            _context.SaveChanges();
        }

        public void DeleteGenre(int id)
        {
            var genre = GetGenreById(id);
            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }

        public IEnumerable<Book> GetAll()
        {
            var books = _context.Books.ToList();
            return books;

        }

        public IEnumerable<Author> GetAllAuthors()
        {
            var authors = _context.Authors.ToList();
            return authors;

        }

        public IEnumerable<Book> GetAllBooks()
        {
            var books = _context.Books.Include(x=>x.Publisher).Include(x=>x.Genre).ToList();
            return books;

        }

        public IEnumerable<Genre> GetAllGenres()
        {
            var genre = _context.Genres.ToList();
            return genre;
        }

        public Author? GetAuthorById(int id)
        {
            var author = _context.Authors.Find(id);
            return author;
        }

        public Book? GetBookById(int id)
        {
            var book = _context.Books.Find(id);
            return book;
        }

        public Genre? GetGenreById(int id)
        {
            var genre = _context.Genres.Find(id);
            return genre;
        }

        public void Update(Book books)
        {
            _context.Books.Update(books);
            _context.SaveChanges();
        }

        public void Update(Genre genre)
        {
            _context.Genres.Update(genre);
            _context.SaveChanges();
        }

        public void Update(Author author)
        {
            _context.Authors.Update(author);
            _context.SaveChanges();
        }
    }
}