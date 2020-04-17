using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MantiScanServices.Common;
using MantiScanServices.Model;
using MantiScanServices.Model.Organizations;
using MantiScanServices.Model.Users;
using MantiScanServices.ViewModel.Operation;
using MantiScanServices.ViewModel.Organization;
using MantiScanServices.ViewModel.Users;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MantiScanServices.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    public class OrganizationController : BaseController
    {
        public IRepository<Organization> OrganizationRepository { get; set; }
        public IRepository<User> UserRepository { get; set; }
        private UserManager<User> UserManager;
        private IRepository<UserRoleDetail> UserRoleDetail;
        private readonly AnraConfiguration AnraConfiguration;

        public OrganizationController(
            [FromServices]IRepository<Organization> organizationRepository,
            UserManager<User> userManager,
            [FromServices] IRepository<UserRoleDetail> userRoleDetail,            
             [FromServices] IRepository<User> userRepository, AnraConfiguration anraConfiguration)
        {
            OrganizationRepository = organizationRepository;
            UserManager = userManager;
            UserRoleDetail = userRoleDetail;            
            AnraConfiguration = anraConfiguration;
            UserRepository = userRepository;
        }

        [HttpGet("{id}")]
        public OrganizationItem Get(long id)
        {
            var newOrganizationItem = OrganizationRepository.Find(id).Adapt<Organization, OrganizationItem>();

            #region To get admin user details

            var user = UserRepository.GetAll().FirstOrDefault(x => string.IsNullOrEmpty(x.AdminId) && x.OrganizationId == newOrganizationItem.OrganizationId);

            if (user != null)
            {
                newOrganizationItem.Email = user.Email;
                newOrganizationItem.FirstName = user.FirstName;
                newOrganizationItem.LastName = user.LastName;
            }

            #endregion To get admin user details

            return newOrganizationItem;
        }

        [HttpGet("listing"), ActionName("listing")]
        public List<OrganizationListItem> Get()
        {
            var organizations = OrganizationRepository.GetAll(UserEmail).Where(v => v.Name != AnraConfiguration.DefaultOrganization);
            return organizations.Adapt<IEnumerable<Organization>, List<OrganizationListItem>>();
        }

        [HttpGet("NewOrganization"), ActionName("NewOrganization")]
        public OrganizationItem GetNewItem()
        {
            var newItem = new OrganizationItem
            {
                
            };

            return newItem;
        }

        [HttpPost]
        [Route("save")]
        public OperationResult Save([FromBody]OrganizationItem organization)
        {
            var operationResult = new OperationResult { IsSuccess = true };

            try
            {
                if (organization.OrganizationId > 0)
                {
                    var existingItem = OrganizationRepository.Find(organization.OrganizationId);
                    organization.Adapt<OrganizationItem, Organization>(existingItem);
                    OrganizationRepository.Update(existingItem);
                    operationResult.SuccessMessage = "Organization Updated Successfully.";
                }
                else
                {   
                    var item = organization.Adapt<OrganizationItem, Organization>();
                    OrganizationRepository.Add(item);

                    #region create admin user for this organization

                    var newUser = organization.Adapt<UserItem, User>();
                    newUser.Id = Guid.NewGuid().ToString();
                    newUser.UserName = organization.Email;
                    newUser.PhoneNumber = organization.ContactPhone;                    
                    newUser.OrganizationId = item.OrganizationId;
                    newUser.IsActive = true;

                    var result = UserManager.CreateAsync(newUser, organization.Password).Result;
                    if (result.Succeeded)
                    {
                        UserRoleDetail.Add(new UserRoleDetail { RoleId = Convert.ToInt32(Roles.Admin), UserId = newUser.Id, IsActive = true });//admin role
                        operationResult.IsSuccess = true;                        
                        operationResult.SuccessMessage = "Organization Created Successfully.";
                    }
                    else
                    {
                        OrganizationRepository.Remove(item.OrganizationId);
                        operationResult.IsSuccess = false;
                        operationResult.ErrorMessage = result.Errors.Select(p => p.Description).FirstOrDefault();
                    }

                    #endregion create admin user for this organization
                }
            }
            catch (Exception ex)
            {
                operationResult.IsSuccess = false;
                operationResult.ErrorMessage = ex.Message;
            }

            return operationResult;
        }

        [HttpDelete("{id}")]
        public OperationResult Delete(long id)
        {
            var result = new OperationResult { IsSuccess = false };
            try
            {
                if (id > 0)
                {
                    OrganizationRepository.Remove(id);
                    result.IsSuccess = true;
                }
                else
                {
                    result.ErrorMessage = "An error occurred while deleting the item.";
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
    }
}