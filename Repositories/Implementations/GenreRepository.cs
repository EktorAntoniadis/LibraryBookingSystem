using LibraryBookingSystem.Models;

namespace LibraryBookingSystem.Repositories.Implementations
{
    public class GenreRepository : IGenreRepository
    {
        private LibraryManagementDbContext _context;
        public GenreRepository(LibraryManagementDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void Add(Genre genre)
        {
            _context.Genres.Add(genre);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var genre = GetById(id);
            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }

        public IEnumerable<Genre> GetAll()
        {
            var genre = _context.Genres.ToList();
            return genre;

        }

        public Genre? GetById(int id)
        {
            var genre = _context.Genres.Find(id);
            return genre;
        }

        public void Update(Genre genre)
        {
            _context.Genres.Update(genre);
            _context.SaveChanges();
        }
    }
}