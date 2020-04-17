using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MantiScanServices.ViewModel.OilSpillReport
{
    [DataContract]
    public class NotificationAgencyView
    {
        /// <summary>
        /// not defined
        /// </summary>
        /// <value>not defined</value>
        [DataMember(Name = "reportedBy", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "reportedBy")]
        public string ReportedBy { get; set; }

        /// <summary>
        /// not defined
        /// </summary>
        /// <value>not defined</value>
        [DataMember(Name = "reportedTo", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "reportedTo")]
        public string ReportedTo { get; set; }

        /// <summary>
        /// not defined
        /// </summary>
        /// <value>not defined</value>
        [DataMember(Name = "agency", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "agency")]
        public string Agency { get; set; }

        /// <summary>
        /// not defined
        /// </summary>
        /// <value>not defined</value>
        [DataMember(Name = "incidentNo", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "incidentNo")]
        public string IncidentNo { get; set; }

        /// <summary>
        /// not defined
        /// </summary>
        /// <value>not defined</value>
        [DataMember(Name = "timeDate", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "timeDate")]
        public DateTime TimeDate { get; set; }
    }
}
