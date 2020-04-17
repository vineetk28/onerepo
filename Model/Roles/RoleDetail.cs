using MantiScanServices.Model.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MantiScanServices.Model.Roles
{
    public class RoleDetail : ModelBase
    {
        public int RoleDetailId { get; set; }
        public bool View { get; set; }
        public bool Delete { get; set; }
        public bool Edit { get; set; }
        public bool Add { get; set; }
        public bool IsActive { get; set; }

        public int RoleId { get; set; }
        public int ModuleId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        [ForeignKey("ModuleId")]
        public Module Module { get; set; }
    }
}
