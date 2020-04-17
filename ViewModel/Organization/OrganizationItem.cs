using MantiScanServices.ViewModel.Lookup;
using MantiScanServices.ViewModel.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantiScanServices.ViewModel.Organization
{
    public class OrganizationItem : UserItem
    {  
        public new int OrganizationId { get; set; }        
        public string Name { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string LogoFile { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
    }
}
