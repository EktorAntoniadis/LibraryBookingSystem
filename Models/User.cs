using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryBookingSystem.Models
{
    public class User
    {
        [Key]        
        public int UserId { get; set; }
        public int FirstName { get; set; }
        public int LastName { get; set; }
        public int Email { get; set; }
        public int Phone { get; set; }
        public int RegisteredDate { get; set; }
       
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
