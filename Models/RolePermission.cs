using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryBookingSystem.Models
{
    public class RolePermission
    {
        [Key]
        public int RolePermissionId { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }

        public Role Role { get; set; }

        [ForeignKey("Permission")]
        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
