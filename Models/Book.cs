using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryBookingSystem.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        [ForeignKey("Publisher")]
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public DateOnly PublicationDate { get; set; }
        public string ISBN { get; set; }
       
        [ForeignKey("Genre")]        
        public int GenreID { get; set; }
        public Genre Genre { get; set; }
        public int Pages { get; set; }
        public string Language { get; set; }
        public string Summary { get; set; }
        public int Rating { get; set; }
        public virtual ICollection<Author> Authors { get; set; }

    }
}
