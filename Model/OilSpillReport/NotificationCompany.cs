using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MantiScanServices.Model.OilSpillReport
{
    public class NotificationCompany : ModelBase
    {
        [Key]
        public int NotificationCompanyId { get; set; }
        public string ReportedBy { get; set; }
        public string ReportedToName { get; set; }
        public string ReportedToPosition { get; set; }
        public DateTime TimeDate { get; set; }

        public int OilSpillReportId { get; set; }
        [ForeignKey(nameof(OilSpillReportId))]
        public virtual OilSpillReport OilSpillReport { get; set; }
    }
}
