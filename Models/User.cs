using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryBookingSystem.Models
{
    public class User
    {
        [Key]        
        public int UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }
        public DateOnly RegisteredDate { get; set; }
       
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public Role Role { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }
        
    }
}
