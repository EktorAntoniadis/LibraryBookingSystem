using LibraryBookingSystem.Models;


namespace LibraryBookingSystem.Repositories.Implementations
{
    public class AuthorRepository
    {
        private LibraryManagementDbContext _context;

        public AuthorRepository(LibraryManagementDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void Add(Author author)
        {
            _context.Authors.Add(author);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var author = GetById(id);
            _context.Authors.Remove(author);
            _context.SaveChanges();
        }

        public IEnumerable<Author> GetAll()
        {
            var authors = _context.Authors.ToList();
            return authors;

        }

        public Author? GetById(int id)
        {
            var author = _context.Authors.Find(id);
            return author;
        }

        public void Update(Author authors)
        {
            _context.Authors.Update(authors);
            _context.SaveChanges();
        }
    }
}