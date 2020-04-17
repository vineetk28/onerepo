using MantiScanServices.Model.Incidents;
using MantiScanServices.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantiScanServices.Model.Organizations
{
    public class Organization : ModelBase
    {
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }        
        public string LogoFile { get; set; }        
        public string Website { get; set; }
        public string Address { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Incident> Incidents { get; set; }
    }
}
