using MantiScanServices.Model.Organizations;
using MantiScanServices.Model.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MantiScanServices.Model.Plateforms
{
    public class PlateForm
    {
        [Key]
        public int PlateFormId { get; set; }                
        public string AreaCode { get; set; }
        public string BlockNumber { get; set; }
        public string Field { get; set; }
        public string StructureName { get; set; }
        public string StructureNumber { get; set; }
        public string StructureTypeCode { get; set; }
        public string AuthorityType { get; set; }
        public string AuthorityNumber { get; set; }
        public string AuthorityStatus { get; set; }
        public string BusAscName { get; set; }
        public DateTime InstallDate { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public int OrganizationId { get; set; }
        [ForeignKey(nameof(OrganizationId))]
        public Organization Organization { get; set; }

    }
}
