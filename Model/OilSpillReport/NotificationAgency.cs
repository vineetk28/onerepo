using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MantiScanServices.Model.OilSpillReport
{
    public class NotificationAgency : ModelBase
    {
        [Key]
        public int NotificationAgencyId { get; set; }
        public string ReportedBy { get; set; }
        public string ReportedTo { get; set; }
        public string Agency { get; set; }
        public string IncidentNo { get; set; }
        public DateTime TimeDate { get; set; }

        public int OilSpillReportId { get; set; }
        [ForeignKey(nameof(OilSpillReportId))]
        public virtual OilSpillReport OilSpillReport { get; set; }
    }
}
