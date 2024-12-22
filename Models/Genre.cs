using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryBookingSystem.Models
{
    public class Genre
    {
        [Key]
        public int GenreID { get; set; }
        public int GenreName { get; set; }
        public int Description { get; set; }

    }
}
