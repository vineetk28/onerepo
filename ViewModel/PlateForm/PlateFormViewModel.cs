using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MantiScanServices.ViewModel.PlateForm
{
    public class PlateFormViewModel
    {
        [DataMember(Name = "plateFormId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "plateFormId")]
        public int PlateFormId { get; set; }

        [DataMember(Name = "areaCode", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "areaCode")]
        public string AreaCode { get; set; }

        [DataMember(Name = "blockNumber", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "blockNumber")]
        public string BlockNumber { get; set; }

        [DataMember(Name = "field", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "field")]
        public string Field { get; set; }

        [DataMember(Name = "structureName", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "structureName")]
        public string StructureName { get; set; }

        [DataMember(Name = "structureNumber", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "structureNumber")]
        public string StructureNumber { get; set; }

        [DataMember(Name = "structureTypeCode", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "structureTypeCode")]
        public string StructureTypeCode { get; set; }

        [DataMember(Name = "authorityType", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "authorityType")]
        public string AuthorityType { get; set; }

        [DataMember(Name = "authorityNumber", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "authorityNumber")]
        public string AuthorityNumber { get; set; }

        [DataMember(Name = "authorityStatus", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "authorityStatus")]
        public string AuthorityStatus { get; set; }

        [DataMember(Name = "busAscName", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "busAscName")]
        public string BusAscName { get; set; }

        [DataMember(Name = "installDate", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "installDate")]
        public DateTime InstallDate { get; set; }

        [DataMember(Name = "latitude", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "latitude")]
        public string Latitude { get; set; }

        [DataMember(Name = "longitude", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "longitude")]
        public string Longitude { get; set; }

        [DataMember(Name = "userId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        [DataMember(Name = "organizationId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "organizationId")]
        public int OrganizationId { get; set; }
    }
}
