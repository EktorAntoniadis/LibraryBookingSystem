﻿using System.ComponentModel.DataAnnotations;

namespace LibraryBookingSystem.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public string Description { get; set; }

        public virtual IEnumerable<Permission> Permissions { get; set; }
    }
}
