using Bogus;
using Microsoft.AspNetCore.Identity;
using System.Numerics;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraryBookingSystem.Models
{
    public class DatabaseSeed
    {
        private readonly LibraryManagementDbContext _context;
        private PasswordHasher<User> _hasher;

        public DatabaseSeed(LibraryManagementDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _hasher = new PasswordHasher<User>();
        }

        public void Seed()
        {
            var permisionAddAbook = new Permission
            {
                PermissionName = "Add a book",
                Description = "Permission to add a book",

            };

            var permisionDeleteABook = new Permission
            {
                PermissionName = "Delete a book",
                Description = "Permission to Delete a book",

            };

            var permisionUpdateABook = new Permission
            {
                PermissionName = "Read a book",
                Description = "Permission to Read a book",

            };

            var permisionReadABook = new Permission
            {
                PermissionName = "Delete a book",
                Description = "Permission to aDelete a book",
            };

            var permisionReadBooks = new Permission
            {
                PermissionName = "Read books",
                Description = "Permission to Read books",

            };

            var permisionAddMember = new Permission
            {
                PermissionName = "Add Member",
                Description = "Permission to Add Member",

            };

            var permisionUpdateMember = new Permission
            {
                PermissionName = "Update Member",
                Description = "Permission to Update Member",

            };

            var permisionDeleteMember = new Permission
            {
                PermissionName = "Delete Member",
                Description = "Permission to Delete Member",

            };

            var permisionReadMember = new Permission
            {
                PermissionName = "Read Member",
                Description = "Permission to Read Member",

            };

            var permisionReadMembers = new Permission
            {
                PermissionName = "Read Members",
                Description = "Permission to Read Members",

            };

            if (!_context.Permissions.Any())
            {
                _context.Permissions.AddRange(permisionAddAbook, permisionDeleteABook, permisionUpdateABook, permisionReadABook, permisionReadBooks, permisionAddMember, permisionUpdateMember, permisionDeleteMember, permisionReadMember, permisionReadMembers);
                _context.SaveChanges();
            }

            var roleLibrarian = new Role
            {
                RoleName = "Librarian",
                Description = "This is the role of a librarian",
                Permissions = new List<Permission>()
                  {
                      permisionAddAbook,
                      permisionAddMember,
                      permisionUpdateABook,
                      permisionUpdateMember,
                      permisionReadMember,
                      permisionReadMembers,
                      permisionDeleteABook
                  }
            };

            var roleAdministrator = new Role
            {
                RoleName = "Administrator",
                Description = "This is the role of a adminstrator",
                Permissions = new List<Permission>()
                  {
                      permisionAddAbook,
                      permisionAddMember,
                      permisionDeleteMember,
                      permisionReadMember,
                      permisionReadMembers,

                  }
            };

            var roleMember = new Role
            {
                RoleName = "Member",
                Description = "This is the role of a member",
                Permissions = new List<Permission>()
                  {
                      permisionReadABook,
                      permisionReadBooks,
                      permisionReadMember
                  }
            };


            if (!_context.Roles.Any())
            {
                _context.Roles.AddRange(roleLibrarian, roleAdministrator, roleMember);
                _context.SaveChanges();
            }

            var memberUser = new User
            {
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@example.com",
                PhoneNumber = "1234567890",
                RegisteredDate = new DateOnly(2020, 5, 20),
                Username = "jdoe",
                Role = roleMember
            };

            memberUser.Password = _hasher.HashPassword(memberUser, "!memberUser!1234");

            var administratorUser = new User
            {
                FirstName = "Han",
                LastName = "Solo",
                Email = "Han.Solo@example.com",
                PhoneNumber = "9723014857",
                RegisteredDate = new DateOnly(1990, 3, 12),
                Username = "hsolo",
                Role = roleAdministrator
            };

            administratorUser.Password = _hasher.HashPassword(administratorUser, "!administratorUser!5678");

            var librarianUser = new User
            {
                FirstName = "Igor",
                LastName = "Karkaroff",
                Email = "Igor.Karkaroff@example.com",
                PhoneNumber = "3017307376",
                RegisteredDate = new DateOnly(1991, 3, 15),
                Username = "ikark",
                Role = roleLibrarian
            };
            librarianUser.Password = _hasher.HashPassword(librarianUser, "!librarianUser!9628");

            if (!_context.Users.Any())
            {
                _context.Users.AddRange(memberUser, administratorUser, librarianUser);
                _context.SaveChanges();
            }

            //Βαλε μου εγγραφες στον πινακα Genres οπως εχουμε κανει και για τα παραπάνω, με τα εξης είδη:
            //Romance, Mystery, Horror, Drama, Thriller, Adventure, Fiction, Novel, Fantasy, Biography, Humor, Science, History

            var genreList = new List<Genre>
            {
                new Genre
                {
                    GenreName = "Romance",
                    Description = "Romance Genre"
                },
                new Genre
                {
                  GenreName = "Mystery",
                  Description = "Mystery"
                },
                new Genre
                {
                  GenreName = "Horror",
                  Description = "Horror"
                },
                new Genre
                {
                  GenreName = "Drama",
                  Description = "Drama"
                },
                new Genre
                {
                  GenreName = "Thriller",
                  Description = "Thriller"
                },
                new Genre
                {
                  GenreName = "Adventure",
                  Description = "Adventure"
                },
                new Genre
                {
                  GenreName = "Fiction",
                  Description = "Fiction"
                },
                new Genre
                {
                  GenreName = "Novel",
                  Description = "Novel"
                },
                new Genre
                {
                  GenreName = "Fantasy",
                  Description = "Fantasy"
                },
                new Genre
                {
                  GenreName = "Biography",
                  Description = "Biography"
                },
                new Genre
                {
                  GenreName = "Humor",
                  Description = "Humor"
                },
                new Genre
                {
                  GenreName = "Science",
                  Description = "Science"
                },
                new Genre
                {
                  GenreName = "History",
                  Description = "History"
                },
            };

            if (!_context.Genres.Any())
            {
                _context.Genres.AddRange(genreList);
                _context.SaveChanges();
            }

            SeedBogusData();
        }

        private void SeedBogusData()
        {
            if (_context.Authors.Any() || _context.Publishers.Any() || _context.Books.Any())
            {
                return;
            }

            var libraryFaker = new Faker();

            var publisherList = new List<Publisher>();

            for (int i = 0; i < 1000; i++)
            {
                publisherList.Add(new Publisher
                {
                    Name = libraryFaker.Company.CompanyName(),
                    Address = libraryFaker.Address.StreetAddress(),
                    City = libraryFaker.Address.City(),
                    Country = libraryFaker.Address.Country(),
                    Email = libraryFaker.Internet.Email(),
                    Phone = libraryFaker.Phone.PhoneNumber(),
                    Website = libraryFaker.Internet.Url(),
                });
            }

            _context.Publishers.AddRange(publisherList);

            var authorlist = new List<Author>();

            for (int i = 0; i < 1000; i++)
            {
                authorlist.Add(new Author
                {
                    FirstName = libraryFaker.Name.FirstName(),
                    LastName = libraryFaker.Name.LastName(),
                });
            }

            _context.Authors.AddRange(authorlist);
            _context.SaveChanges();

            var bookList = new List<Book>();

            for (int i = 0; i < 1000; i++)
            {
                var randomNumberOfAuthors = libraryFaker.Random.Int(1, 5);
                bookList.Add(new Book
                {
                    Title = libraryFaker.Lorem.Random.Word(),
                    PublicationDate = DateOnly.FromDateTime(libraryFaker.Date.Past(30)),
                    ISBN = libraryFaker.Random.ReplaceNumbers("###-#-##-######-#"),
                    GenreID = libraryFaker.Random.Int(1, 13),
                    Pages = libraryFaker.Random.Int(50, 800),
                    Language = libraryFaker.PickRandom(new[] { "English", "French", "German", "Greek", "Spanish" }),
                    Rating = libraryFaker.Random.Int(1, 5),
                    PublisherId = libraryFaker.Random.Int(1, 1000),
                    Authors = libraryFaker.PickRandom(authorlist, randomNumberOfAuthors).ToList(),
                    Summary = libraryFaker.Lorem.Paragraph(),
                });
            }

            _context.Books.AddRange(bookList);
            _context.SaveChanges();
        }
    }
}
