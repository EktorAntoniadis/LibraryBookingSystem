using LibraryBookingSystem.Models;

namespace LibraryBookingSystem.Repositories.Implementations
{
    public class BookRepository
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

        public void Delete(int id)
        {
            var book = GetById(id);
            _context.Books.Remove(book);
            _context.Books.Remove(book);
            _context.SaveChanges();
        }

        public IEnumerable<Book> GetAll()
        {
            var books = _context.Books.ToList();
            return books;

        }

        public Book? GetById(int id)
        {
            var book = _context.Books.Find(id);
            return book;
        }

        public void Update(Book books)
        {
            _context.Books.Update(books);
            _context.SaveChanges();
        }
    }
}