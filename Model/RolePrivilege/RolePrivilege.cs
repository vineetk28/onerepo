using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantiScanServices.Model.RolePrivilege
{
    public class RolePrivilege
    {
        public int RolePrivilegeId { get; set; }
        public int RoleId { get; set; }
        public int PrivilegeId { get; set; }
    }
}
