using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantiScanServices.Model.Modules
{
    public class Module : ModelBase
    {
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public bool IsActive { get; set; }
    }
}
