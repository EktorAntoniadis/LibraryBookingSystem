using LibraryBookingSystem.Common;
using LibraryBookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryBookingSystem.Repositories.Implementations
{
    public class BookRepository : IBookRepository
    {
        private LibraryManagementDbContext _context;
        public BookRepository(LibraryManagementDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void AddBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void AddGenre(Genre genre)
        {
            _context.Genres.Add(genre);
            _context.SaveChanges();
        }

        public void AddAuthor(Author author)
        {
            _context.Authors.Add(author);
            _context.SaveChanges();
        }

        public void AddPublisher(Publisher publisher)
        {
            _context.Publishers.Add(publisher);
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

        public void DeletePublisher(int id)
        {
            var publisher = GetPublisherById(id);
            if (publisher != null)
            {
                _context.Publishers.Remove(publisher);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            var authors = _context.Authors.ToList();
            return authors;

        }

        public IEnumerable<Book> GetBooks()
        {
            var books = _context.Books.Include(x => x.Publisher).Include(x => x.Genre).ToList();
            return books;

        }

        public IEnumerable<Genre> GetAllGenres()
        {
            var genre = _context.Genres.ToList();
            return genre;
        }

        public IEnumerable<Publisher> GetAllPublishers()
        {
            var publishers = _context.Publishers.ToList();
            return publishers;
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

        public Publisher? GetPublisherById(int id)
        {
            var publisher = _context.Publishers.Find(id);
            return publisher;
        }

        public void UpdateBook(Book books)
        {
            _context.Books.Update(books);
            _context.SaveChanges();
        }

        public void UpdateGenre(Genre genre)
        {
            _context.Genres.Update(genre);
            _context.SaveChanges();
        }

        public void UpdateAuthor(Author author)
        {
            _context.Authors.Update(author);
            _context.SaveChanges();
        }

        public void UpdatePublisher(Publisher publisher)
        {
            _context.Publishers.Update(publisher);
            _context.SaveChanges();
        }

        public PaginatedList<Book> GetBooks(
            int pageIndex,
            int pageSize,
            string? searchTitle = null,
            string? genre = null,
            string? author = null,
            string? ISBN = null,
            string? sortColumn = "Title",
            string? sortDirection = "asc")
        {
            var query = _context.Books
                .Include(x => x.Genre)
                .Include(x => x.Authors)
                .Include(x=>x.Publisher)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTitle))
            {
                query = query.Where(x => x.Title.Contains(searchTitle));
            }

            if (!string.IsNullOrWhiteSpace(ISBN))
            {
                query = query.Where(x => x.ISBN.Contains(ISBN));
            }

            if (!string.IsNullOrWhiteSpace(genre))
            {
                query = query.Where(x => x.Genre.GenreName.Contains(genre));
            }

            if (!string.IsNullOrWhiteSpace(author))
            {
                query = query.Where(x => x.Authors
                .Any(x => (x.FirstName + " " + x.LastName).Contains(author)
                || x.FirstName.Contains(author)
                || x.LastName.Contains(author)));
            }

            switch (sortColumn)
            {
                case "Title":
                    query = sortDirection == "desc" ? query.OrderByDescending(x => x.Title) : query.OrderBy(x => x.Title);
                    break;
                case "genre":
                    query = sortDirection == "desc" ? query.OrderByDescending(x=>x.Genre.GenreName): query.OrderBy(x => x.Genre.GenreName);
                    break;
            }

            var totalRecords = query.Count();

            var books = query.Skip((pageIndex-1) * pageSize).Take(pageSize).ToList();

            return new PaginatedList<Book>(books, totalRecords, pageIndex, pageSize);
        }
    }
}