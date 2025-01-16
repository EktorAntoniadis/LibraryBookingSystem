﻿using LibraryBookingSystem.Common;
using LibraryBookingSystem.Models;

namespace LibraryBookingSystem.Repositories
{
    public interface IBookRepository
    {
        PaginatedList<Book> GetBooks(
            int pageIndex, 
            int pageSize, 
            string? searchTitle = null,
            string? genre = null, 
            string? author  = null,
            string? ISBN = null,
            string? sortColumn = "Title",
            string? sortDirection = "asc");
        Book? GetBookById(int id);
        void AddBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(int id);
        IEnumerable<Genre> GetAllGenres();
        Genre? GetGenreById(int id);
        void AddGenre(Genre genre);
        void UpdateGenre(Genre genre);
        void DeleteGenre(int id);
        IEnumerable<Author> GetAllAuthors();
        Author? GetAuthorById(int id);
        void AddAuthor(Author author);
        void UpdateAuthor(Author author);
        void DeleteAuthor(int id);

        IEnumerable<Publisher> GetAllPublishers();
        Publisher? GetPublisherById(int id);
        void AddPublisher(Publisher publisher);
        void UpdatePublisher(Publisher publisher);
        void DeletePublisher(int id);
    }
}
