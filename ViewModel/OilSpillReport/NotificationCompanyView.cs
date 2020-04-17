using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MantiScanServices.ViewModel.OilSpillReport
{
    [DataContract]
    public class NotificationCompanyView
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
        [DataMember(Name = "reportedToName", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "reportedToName")]
        public string ReportedToName { get; set; }

        /// <summary>
        /// not defined
        /// </summary>
        /// <value>not defined</value>
        [DataMember(Name = "reportedToPosition", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "reportedToPosition")]
        public string ReportedToPosition { get; set; }

        /// <summary>
        /// not defined
        /// </summary>
        /// <value>not defined</value>
        [DataMember(Name = "timeDate", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "timeDate")]
        public DateTime TimeDate { get; set; }
    }
}
