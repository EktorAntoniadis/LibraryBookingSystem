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
            var author = _context.Authors
                .Include(x=>x.Books)
                .ThenInclude(x=>x.Genre)
                .FirstOrDefault(x=>x.AuthorId == id);
            return author;
        }

        public Book? GetBookById(int id)
        {
            var book = _context.Books
                .Include(x => x.Genre)
                .Include(x=>x.Publisher)
                .FirstOrDefault(x => x.BookId == id);
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
                case "Genre":
                    query = sortDirection == "desc" ? query.OrderByDescending(x=>x.Genre.GenreName): query.OrderBy(x => x.Genre.GenreName);
                    break;
                case "Rating":
                    query = sortDirection == "desc" ? query.OrderByDescending(x => x.Rating) : query.OrderBy(x => x.Rating);
                    break;
                default:
                    query = sortDirection == "desc" ? query.OrderByDescending(x => x.Title) : query.OrderBy(x => x.Title);
                    break;
            }

            var totalRecords = query.Count();

            var books = query.Skip((pageIndex-1) * pageSize).Take(pageSize).ToList();

            return new PaginatedList<Book>(books, totalRecords, pageIndex, pageSize);
        }

        public PaginatedList<Author> GetAuthors(
            int pageIndex, 
            int pageSize,
            string? firstName,
            string? lastName,
            string? sortColumn = "LastName", 
            string? sortDirection = "asc")
        {
            var query = _context.Authors.AsQueryable();

            if (!string.IsNullOrWhiteSpace(firstName))
            {
                query  = query.Where(x=>x.FirstName.Contains(firstName));
            }

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                query = query.Where(x => x.LastName.Contains(lastName));
            }

            if(!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
            {
                query = query.Where(x=> x.FirstName.Contains(firstName) && x.LastName.Contains(lastName));
            }

            switch (sortColumn)
            {
                case "FirstName":
                    query = sortDirection == "desc" ? query.OrderByDescending(x => x.FirstName) : query.OrderBy(x => x.FirstName);
                    break;
                default:
                    query = sortDirection == "desc" ? query.OrderByDescending(x => x.LastName) : query.OrderBy(x => x.LastName);
                    break;
            }

            var totalRecords = query.Count();

            var authors = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new PaginatedList<Author>(authors, totalRecords, pageIndex, pageSize);
        }

        public PaginatedList<Publisher> GetPublishers(
            int pageIndex, 
            int pageSize, 
            string? name, 
            string? phone, 
            string? address, 
            string? city, 
            string? country, 
            string? sortColumn = "Name", 
            string? sortDirection = "asc")
        {
            var query = _context.Publishers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(x => x.Name.Contains(name));
            }

            if (!string.IsNullOrWhiteSpace(phone))
            {
                query = query.Where(x => x.Phone.Contains(phone));
            }

            if (!string.IsNullOrWhiteSpace(address))
            {
                query = query.Where(x => x.Address.Contains(address));
            }

            if (!string.IsNullOrWhiteSpace(city))
            {
                query = query.Where(x => x.City.Contains(city));
            }

            if (!string.IsNullOrWhiteSpace(country))
            {
                query = query.Where(x => x.Country.Contains(country));
            }

            switch (sortColumn)
            {
                case "City":
                    query = sortDirection == "desc" ? query.OrderByDescending(x => x.City) : query.OrderBy(x => x.City);
                    break;
                case "Country":
                    query = sortDirection == "desc" ? query.OrderByDescending(x => x.Country) : query.OrderBy(x => x.Country);
                    break;
                default:
                    query = sortDirection == "desc" ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
                    break;
            }

            var totalRecords = query.Count();

            var publishers = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new PaginatedList<Publisher>(publishers, totalRecords, pageIndex, pageSize);
        }
    }
}