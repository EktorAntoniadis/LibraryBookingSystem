using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryBookingSystem.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public int Title { get; set; }
        public int Publisher { get; set; }
        public int PublicationDate { get; set; }
        public int ISBN { get; set; }
       
        [ForeignKey("Genre")]        
        public int GenreID { get; set; }
        public Genre Genre { get; set; }
        public int Pages { get; set; }
        public int Language { get; set; }
        public int Summary { get; set; }
        public int Rating { get; set; }
        public virtual IEnumerable<Author> Authors { get; set; }

    }
}
