using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryBookingSystem.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        public int FirstName { get; set; }
        public int LastName { get; set; }

        public virtual IEnumerable<Book> Books { get; set; }

    }
}
