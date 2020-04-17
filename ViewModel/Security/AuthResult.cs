using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantiScanServices.ViewModel.Security
{
    public class AuthResult
    {
        public bool IsAuthenticated { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public DateTime? TokenExpires { get; set; }
        public string UserName { get; set; }
        public string Roles { get; set; }        
        public string ProfilePhoto { get; set; }
        public string UserEmail { get; set; }
        public string OrgLogo { get; set; }         
        public string Message { get; set; }
    }
}
