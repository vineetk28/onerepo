using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MantiScanServices.ViewModel.Incident
{
    public class IncidentViewModel
    {
        [DataMember(Name = "incidentId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "incidentId")]
        public int IncidentId { get; set; }

        [DataMember(Name = "incidentName", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "incidentName")]
        public string IncidentName { get; set; }

        [DataMember(Name = "incidentDate", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "incidentDate")]
        public DateTime IncidentDate { get; set; }

        [DataMember(Name = "incidentCauseId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "incidentCauseId")]
        public int IncidentCauseId { get; set; }

        [DataMember(Name = "otherDamage", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "otherDamage")]
        public string OtherDamage { get; set; }

        [DataMember(Name = "notes", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "notes")]
        public string Notes { get; set; }

        [DataMember(Name = "towerId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "towerId")]
        public string TowerId { get; set; }

        [DataMember(Name = "organizationId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "organizationId")]
        public int OrganizationId { get; set; }

        [DataMember(Name = "userId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }
    }
}
