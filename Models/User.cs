using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryBookingSystem.Models
{
    public class User
    {
        [Key]        
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateOnly RegisteredDate { get; set; }
       
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        
    }
}
