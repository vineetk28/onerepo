using MantiScanServices.Model.Organizations;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MantiScanServices.Model.Users
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public int OrganizationId { get; set; }
        public string AdminId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime LastLogin { get; set; }

        [ForeignKey(nameof(OrganizationId))]
        public Organization Organization { get; set; }

        public virtual ICollection<UserRoleDetail> Roles { get; set; }
    }
}
