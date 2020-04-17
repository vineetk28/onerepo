using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantiScanServices.ViewModel.Users
{
    public class UserListItem
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        
        public DateTime DateCreated { get; set; }
        public string OrganizationName { get; set; }
        public int OrganizationId { get; set; }

        public string StatusText
        {
            get
            {
                return IsActive ? "Active" : "Inactive";
            }

            set { }
        }

        public bool IsActive { get; set; }

        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
            set { }
        }
    }
}
