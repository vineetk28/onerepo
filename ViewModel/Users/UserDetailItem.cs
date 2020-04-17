using MantiScanServices.ViewModel.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantiScanServices.ViewModel.Users
{
    public class UserDetailItem
    {
        public String UserName { get; set; }
        public String UserEmail { get; set; }
        public List<RoleItem> UserRoles { get; set; }        
        public String PhoneNumber { get; set; }
    }
}
