using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MantiScanServices.ViewModel
{
    [DataContract]
    public class EmailTestViewModel
    {
        /// <summary>
        /// reciever email id
        /// </summary>
        /// <value>email id of reciever</value>
        [DataMember(Name = "email_to", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "email_to")]
        [Required]
        public string EmailTo { get; set; }

        /// <summary>
        /// sender email id
        /// </summary>
        /// <value>email id of sender</value>
        [DataMember(Name = "email_from", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "email_from")]
        [Required]
        public string EmailFrom { get; set; }

        /// <summary>
        /// sender mail id
        /// </summary>
        /// <value>mail id of sender</value>
        [DataMember(Name = "email_body", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "mail_body")]
        [Required]
        public string EmailBody { get; set; }
    }
}
