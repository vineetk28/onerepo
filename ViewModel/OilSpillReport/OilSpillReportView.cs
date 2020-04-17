using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace MantiScanServices.ViewModel.OilSpillReport
{
    [DataContract]
    public class OilSpillReportView
    {
        /// <summary>
        /// Id of specific oil spill report
        /// </summary>
        [DataMember(Name = "oilSpillReportId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "oilSpillReportId")]
        public int OilSpillReportId { get; set; }

        /// <summary>
        /// Name of oil spill report
        /// </summary>        
        [DataMember(Name = "reportName", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "reportName")]
        [Required]
        public string ReportName { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
		/// Not Defined
		/// </summary>
		/// <value>Not Defined.</value>
        [DataMember(Name = "position", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "position")]
        public string Position { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "reportingPhone", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "reportingPhone")]
        public string ReportingPhone { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "reportingCompany", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "reportingCompany")]
        public string ReportingCompany { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "suspectedCompany", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "suspectedCompany")]
        public string SuspectedCompany { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "suspectedPhone", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "suspectedPhone")]
        public string SuspectedPhone { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "dtIncidentOccurred", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "dtIncidentOccurred")]
        public DateTime DtIncidentOccurred { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "dtIncidentQiIc", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "dtIncidentQiIc")]
        public DateTime DtIncidentQiIc { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "areaBlock", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "areaBlock")]
        public string AreaBlock { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "ocsg", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "ocsg")]
        public string Ocsg { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "facility", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "facility")]
        public string Facility { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "latitude", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "latitude")]
        public string Latitude { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "longitude", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "longitude")]
        public string Longitude { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "nearestCity", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "nearestCity")]
        public string NearestCity { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "distance", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "distance")]
        public int Distance { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "discription", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "discription")]
        public string Discription { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "quantity", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "quantity")]
        public int Quanity { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "qtyUnitId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "qtyUnitId")]
        public string QtyUnitId { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "material", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "material")]
        public string Material { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "apiGravity", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "apiGravity")]
        public string APIgravity { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "length", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "length")]
        public int Length { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "lengthUnitId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "lengthUnitId")]
        public string LengthUnitId { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "width", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "width")]
        public int Width { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "widthUnitId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "widthUnitId")]
        public string WidthUnitId { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "percentageOfSlick", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "percentageOfSlick")]
        public double PercentageOfSlick { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "sourceSecuredAt", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "sourceSecuredAt")]
        public DateTime SourceSecuredAt { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "sourceContinous", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "sourceContinous")]
        public double SourceContinous { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "sourceContinousUnitId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "sourceContinousUnitId")]
        public string SourceContinousUnitId { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "barelyVisible", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "barelyVisible")]
        public double BarelyVisible { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "brightlyColored", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "brightlyColored")]
        public double BrigthlyColored { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "silvery", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "silvery")]
        public double Silvery { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "dull", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "dull")]
        public double Dull { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "slightlyColored", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "slightlyColored")]
        public double SlightlyColored { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "dark", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "dark")]
        public double Dark { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "airTemp", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "airTemp")]
        public int AirTemp { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "waterTemp", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "waterTemp")]
        public int WaterTemp { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "ceiling", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "ceiling")]
        public double Ceiling { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "ceilingUnitId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "ceilingUnitId")]
        public string CeilingUnitId { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "visiblity", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "visiblity")]
        public double Visibility { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "visibilityUnitId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "visibilityUnitId")]
        public string VisibilityUnitId { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "seas", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "seas")]
        public double Seas { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "seasUnitId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "seasUnitId")]
        public string SeasUnitId { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "windDirection", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "windDirection")]
        public double WindDirection { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "windVelocity", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "windVelocity")]
        public string WindVelocity { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "currentDirection", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "currentDirection")]
        public double CurrentDirection { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "currentVelocity", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "currentVelocity")]
        public string CurrentVelocity { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "actionNotes", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "actionNotes")]
        public string ActionNotes { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "injuries", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "injuries")]
        public int Injuries { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "fatalities", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "fatalities")]
        public int Fatalities { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "evacuated", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "evacuated")]
        public int Evacuated { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "damage", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "damage")]
        public int Damage { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "drill", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "drill")]
        public bool Drill { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "actualSpill", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "actualSpill")]
        public bool ActualSpill { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "formPreparedBy", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "formPreparedBy")]
        public string FormPreparedBy { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "nrc", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "nrc")]
        public string NRC { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "captainName", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "captainName")]
        public string CaptainName { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "captainAddress", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "captainAddress")]
        public string CaptainAddress { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "captainPhone", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "captainPhone")]
        public string CaptainPhone { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "vesselName", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "vesselName")]
        public string VesselName { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "vesselOwner", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "vesselOwner")]
        public string VesselOwner { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "cardNo", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "cardNo")]
        public string CardNo { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "callSign", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "callSign")]
        public string CallSign { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "agent", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "agent")]
        public string Agent { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "flag", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "flag")]
        public string Flag { get; set; }

        /// <summary>
        /// Not Defined
        /// </summary>
        /// <value>Not Defined.</value>
        [DataMember(Name = "remarks", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "remarks")]
        public string Remarks { get; set; }

        /// <summary>
		/// plateForm Id of logged in user
		/// </summary>
		/// <value>1,2 or 3</value>
        [DataMember(Name = "plateFormId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "plateFormId")]
        public int PlateFormId { get; set; }

        /// <summary>
		/// Id of logged in user
		/// </summary>
		/// <value>1,2 or 3</value>
        [DataMember(Name = "userId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        /// <summary>
		/// organization Id of logged in user
		/// </summary>
		/// <value>1,2 or 3</value>
        [DataMember(Name = "organizationId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "organizationId")]
        public int OrganizationId { get; set; }

        /// <summary>
		/// not defined
		/// </summary>
		/// <value>not defined</value>
		[DataMember(Name = "notificationCompany", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "notificationCompany")]
        public List<NotificationCompanyView> NotificationCompany { get; set; }

        /// <summary>
		/// not defined
		/// </summary>
		/// <value>not defined</value>
		[DataMember(Name = "notificationAgency", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "notificationAgency")]
        public List<NotificationAgencyView> NotificationAgency { get; set; }

    }
}
