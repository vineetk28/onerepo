using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantiScanServices.Common
{
    public class MailConfiguration
    {
        public string ReceiversEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string CC { get; set; }
    }
}
