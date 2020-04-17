using MantiScanServices.Model.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MantiScanServices.Model.Users
{
    public class UserRoleDetail : ModelBase
    {
        public int UserRoleDetailId { get; set; }
        public bool IsActive { get; set; }

        public string UserId { get; set; }
        public int RoleId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [ForeignKey(nameof(RoleId))]
        public Role Role { get; set; }
    }
}
