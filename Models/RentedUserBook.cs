using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryBookingSystem.Models
{
    public class RentedUserBook
    {
        [Key]
        public int RentedUserBookId { get; set; }

        //Βάλε μου την κλάση που πρέπει για το User.
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        //Βάλε μου την κλάση που πρέπει για το Book.
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int RentedDate { get; set; }
        public int DueDate { get; set; }
        public int ReturnDate { get; set; }
        public int Status { get; set;}
    }
}
