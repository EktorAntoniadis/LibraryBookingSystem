using Bogus;
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
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Password must be between 10 and 15 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
    }
}
