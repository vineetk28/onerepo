using MantiScanServices.Model.Organizations;
using MantiScanServices.Model.Plateforms;
using MantiScanServices.Model.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MantiScanServices.Model.OilSpillReport
{
    public class OilSpillReport : ModelBase
    {
        [Key]
        public int OilSpillReportId { get; set; }
        public string ReportName { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string ReportingPhone { get; set; }
        public string ReportingCompany { get; set; }
        public string SuspectedCompany { get; set; }
        public string SuspectedPhone { get; set; }
        public DateTime DtIncidentOccurred { get; set; }
        public DateTime DtIncidentQiIc { get; set; }
        public string AreaBlock { get; set; }
        public string Ocsg { get; set; }
        public string Facility { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string NearestCity { get; set; }
        public int Distance { get; set; }
        public string Discription { get; set; }
        public int Quanity { get; set; }
        public string QtyUnitId { get; set; }
        public string Material { get; set; }
        public string APIgravity { get; set; }
        public int Length { get; set; }
        public string LengthUnitId { get; set; }
        public int Width { get; set; }
        public string WidthUnitId { get; set; }        
        public double PercentageOfSlick { get; set; }
        public DateTime SourceSecuredAt { get; set; }
        public double SourceContinous { get; set; }
        public string SourceContinousUnitId { get; set; }
        public double BarelyVisible { get; set; }
        public double BrigthlyCovered { get; set; }
        public double Silvery { get; set; }
        public double Dull { get; set; }
        public double SlightlyColored { get; set; }
        public double Dark { get; set; }
        public int AirTemp { get; set; }
        public int WaterTemp { get; set; }
        public double Ceiling { get; set; }
        public string CeilingUnitId { get; set; }
        public double Visibility { get; set; }
        public string VisibilityUnitId { get; set; }
        public double Seas { get; set; }
        public string SeasUnitId { get; set; }
        public double WindDirection { get; set; }
        public string WindVelocity { get; set; }
        public double CurrentDirection { get; set; }
        public string CurrentVelocity { get; set; }
        public string ActionNotes { get; set; }
        public int Injuries { get; set; }
        public int Fatalities { get; set; }
        public int Evacuated { get; set; }
        public int Damage { get; set; }
        public bool Drill { get; set; }
        public bool ActualSpill { get; set; }
        public string FormPreparedBy { get; set; }
        public string NRC { get; set; }
        public string CaptainName { get; set; }
        public string CaptainAddress { get; set; }
        public string CaptainPhone { get; set; }
        public string VesselName { get; set; }
        public string VesselOwner { get; set; }
        public string CardNo { get; set; }
        public string CallSign { get; set; }
        public string Agent { get; set; }
        public string Flag { get; set; }
        public string Remarks { get; set; }

        public virtual ICollection<NotificationCompany> NotificationCompanys { get; set; }
        public virtual ICollection<NotificationAgency> NotificationAgencys { get; set; }

        public int PlateFormId { get; set; }
        [ForeignKey(nameof(PlateFormId))]
        public PlateForm Plateform { get; set; }

        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public int OrganizationId { get; set; }
        [ForeignKey(nameof(OrganizationId))]
        public Organization Organization { get; set; }
    }
}
