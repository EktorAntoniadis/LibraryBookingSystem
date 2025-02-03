using LibraryBookingSystem.Common;
using LibraryBookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryBookingSystem.Repositories.Implementations
{
    public class InventoryRepository : IInventoryRepository
    {
        private LibraryManagementDbContext _context;
        private Inventory inventory;

        public InventoryRepository(LibraryManagementDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddInventory(Inventory inventory)
        {
            _context.Inventories.Add(inventory);
            _context.SaveChanges();
        }

        public void AddRentedBook(RentedUserBook rentedBook)
        {
            _context.RentedUserBooks.Add(rentedBook);
            _context.SaveChanges();
        }

        public void DeleteInventory(int id)
        {
            var inventory = GetInventoryById(id);
            _context.Inventories.Remove(inventory);
            _context.SaveChanges();
        }

        public IEnumerable<Inventory> GetAllInventories()
        {
            var inventories = _context.Inventories.ToList();
            return inventories;
        }

        public IEnumerable<RentedUserBook> GetAllMembersRentedBooks()
        {
            var allMembersRentedBooks = _context
                .RentedUserBooks
                .Include(x => x.Book)
                .Include(x => x.User)                
                .ToList();
            return allMembersRentedBooks;
        }

        public PaginatedList<Inventory> GetInventory(
            int pageIndex,
            int pageSize, 
            int? availableNumberOfCopies, 
            string? bookTitle, 
            string? sortColumn = "Title",
            string? sortDirection = "asc")
        {
            var query = _context.Inventories
                 .Include(x => x.Book)
                 .ThenInclude(x => x.Genre)
                 .AsQueryable();

            if (availableNumberOfCopies != null)
            {
                query = query.Where(x => x.AvailableNumberOfCopies == availableNumberOfCopies);
            }

            if (!string.IsNullOrEmpty(bookTitle))
            {
                query = query.Where(x => x.Book.Title.Contains(bookTitle));
            }

            switch (sortColumn)
            {
                case "AvailableNumberOfCopies":
                    query = sortDirection == "desc" ? query.OrderByDescending(x => x.AvailableNumberOfCopies) : query.OrderBy(x => x.AvailableNumberOfCopies);
                    break;
                default:
                    query = sortDirection == "desc" ? query.OrderByDescending(x => x.Book.Title) : query.OrderBy(x => x.Book.Title);
                    break;
            }

            var totalRecords = query.Count();

            var inventories = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new PaginatedList<Inventory>(inventories, totalRecords, pageIndex, pageSize);
        }

        public Inventory? GetInventoryByBookId(int bookId)
        {
            var inventory = _context.Inventories
                  .Include(x => x.Book)
                 .FirstOrDefault(x => x.BookId == bookId);
            return inventory;
        }

        public Inventory? GetInventoryById(int id)
        {
            var inventory = _context.Inventories.Find(id);
            return inventory;
        }

        public RentedUserBook GetRentedUserBook(int id)
        {
            var rentedUserBook = _context.RentedUserBooks
                .Include(x => x.Book)
                .Include(x => x.User)
                .FirstOrDefault(x => x.RentedUserBookId == id);
            return rentedUserBook;
        }

        public IEnumerable<RentedUserBook> GetUserRentedBooks(int userId)
        {
            var rentedUserBooks = _context.RentedUserBooks
                .Include(x => x.Book)
                .Include(x => x.User)
                .Where(x => x.UserId == userId).ToList();
            return rentedUserBooks;
        }

        public bool IsBookRented(int userId, int bookId)
        {
            var existingRentedBook = _context.RentedUserBooks
                 .Where(x => x.UserId == userId && x.BookId == bookId)
                 .FirstOrDefault();

            return existingRentedBook != null;
        }

        public void UpdateInventory(Inventory inventory)
        {
            _context.Inventories.Update(inventory);
            _context.SaveChanges();
        }

        public void UpdateRentedBook(RentedUserBook rentedBook)
        {
            _context.RentedUserBooks.Update(rentedBook);
            _context.SaveChanges();
        }
    }
}