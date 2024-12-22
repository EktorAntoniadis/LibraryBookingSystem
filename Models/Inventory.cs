using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryBookingSystem.Models
{
    public class Inventory
    {
        [Key]
        public int InventoryID { get; set; }

        //Βάλε μου την ξένη σχέση όπως κάναμε στο User για την άλλη κλάση του Role
        [ForeignKey("Βοοκ")]
        public int BookId { get; set; }
        public Book Book { get; set; } 
        public int TotalNumberOfCopies { get; set; }
        public int AvailableNumberOfCopies { get; set; }

    }
}
