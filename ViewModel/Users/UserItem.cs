using MantiScanServices.ViewModel.Organization;
using MantiScanServices.ViewModel.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantiScanServices.ViewModel.Users
{
    public class UserItem
    {
        Guid _Id;

        public Guid UserId { get; set; }
        public Guid Id
        {
            get { return this._Id; }
            set
            {
                this._Id = value;
                UserId = Id;
            }
        }

        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
            set { }
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        
        public string AdminId { get; set; }
        public string PhoneNumber { get; set; }
        
        public string ProfilePhoto { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        
        public bool IsDeleted { get; set; }

        public List<RoleItem> RoleLookup { get; set; }
        public List<RoleItem> SelectedRole { get; set; }
        public List<OrganizationListItem> OrganizationLookup { get; set; }

        public DateTime DateCreated { get; set; }
        
    }
}
