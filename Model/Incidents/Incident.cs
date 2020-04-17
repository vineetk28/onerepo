using MantiScanServices.Model.Organizations;
using MantiScanServices.Model.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MantiScanServices.Model.Incidents
{
    public class Incident : ModelBase
    {
        public int IncidentId { get; set; }
        public string IncidentName { get; set; }
        public DateTime IncidentDate { get; set; }
        public int IncidentCauseId { get; set; }
        public string OtherDamage { get; set; }
        public string Notes { get; set; }
        public string TowerId { get; set; }
        public int OrganizationId { get; set; }
        public string UserId { get; set; }
        
        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("OrganizationId")]
        public Organization Organization { get; set; }
    }
}
