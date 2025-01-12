using System.ComponentModel.DataAnnotations;

namespace LibraryBookingSystem.Models
{
    public class Permission
    {
        [Key]
        public int PermissionId { get; set; }
        public string PermissionName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
