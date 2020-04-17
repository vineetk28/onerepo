using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MantiScanServices.ViewModel.Master
{
    [DataContract]
    public class TowerViewModel 
    {
        /// <summary>
        /// Id of tower
        /// </summary>
        /// <value>Id of tower</value>
        [DataMember(Name = "tower_id", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "tower_id")]
        public int TowerId { get; set; }

        /// <summary>
        /// Name of tower
        /// </summary>
        /// <value>Name of tower</value>
        [DataMember(Name = "tower_name", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "tower_name")]
        [Required]
        public string TowerName { get; set; }

        /// <summary>
        /// Date of tower creation.
        /// </summary>
        /// <value>Date of tower creation.</value>
        [DataMember(Name = "date_created", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "date_created")]
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// make tower active/inactive.
        /// </summary>
        /// <value>true or false</value>
        [DataMember(Name = "is_active", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "is_active")]
        public bool IsActive { get; set; }

        /// <summary>
        /// delete tower.
        /// </summary>
        /// <value>true or false</value>
        [DataMember(Name = "is_deleted", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "is_deleted")]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Id of user (means super admin)
        /// </summary>
        /// <value>Id of user (means super admin).</value>
        [DataMember(Name = "user_id", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }
    }
}
