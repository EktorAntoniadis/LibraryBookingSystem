using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryBookingSystem.Models
{
    public class RentedUserBook
    {
        [Key]
        public int RentedUserBookId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public Book Book { get; set; }
        public DateOnly RentedDate { get; set; }
        public DateOnly DueDate { get; set; }
        public DateOnly? ReturnDate { get; set; }
        public string Status { get; set;}

        [NotMapped]
        public bool IsOverdue => DateOnly.FromDateTime(DateTime.Now) > DueDate;

        [NotMapped]
        public int OverdueDays => IsOverdue ? (DateOnly.FromDateTime(DateTime.Today).DayNumber - DueDate.DayNumber) : 0;

    }
}
