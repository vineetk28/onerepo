using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantiScanServices.ViewModel.Users
{
    public class AccessPermission
    {
        public bool View { get; set; }

        public bool Delete { get; set; }

        public bool Edit { get; set; }

        public bool Add { get; set; }
    }
}
