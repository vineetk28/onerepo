using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MantiScanServices.Common;
using MantiScanServices.DataProvider;
using MantiScanServices.Model;
using MantiScanServices.Model.Modules;
using MantiScanServices.Model.Organizations;
using MantiScanServices.Model.Roles;
using MantiScanServices.Model.Users;
using MantiScanServices.ViewModel.Operation;
using MantiScanServices.ViewModel.Organization;
using MantiScanServices.ViewModel.Roles;
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
    public class UsersController : BaseController
    {
        private readonly MantiDbContext dbContext;
        public IRepository<User> UserRepository { get; set; }
        public IRepository<IdentityRole> RoleRepository { get; set; }
        public IRepository<Organization> OrganizationRepositoy { get; set; }
        private UserManager<User> userManager;
        private RoleManager<IdentityRole> roleManager;        
        private IRepository<Role> AnraRole;
        private IRepository<RoleDetail> RoleDetail;
        private IRepository<UserRoleDetail> UserRoleDetail;
        private IRepository<Module> Module;
        private readonly AnraConfiguration _anraConfiguration;

        public UsersController([FromServices] UserManager<User> userManager,
            [FromServices] RoleManager<IdentityRole> roleManager,            
            [FromServices] IRepository<User> userRepository,
             [FromServices] IRepository<IdentityRole> roleRepositoy,
             [FromServices] IRepository<Organization> organizationRepositoy,
             [FromServices] IRepository<Role> anraRole,
             [FromServices] IRepository<RoleDetail> roleDetail,
             [FromServices] IRepository<UserRoleDetail> userRoleDetail,             
             AnraConfiguration anraConfiguration,
             [FromServices] IRepository<Module> module,
             MantiDbContext dbContext             
            )
        {
            this.userManager = userManager;
            this.roleManager = roleManager;            
            UserRepository = userRepository;
            RoleRepository = roleRepositoy;
            OrganizationRepositoy = organizationRepositoy;
            AnraRole = anraRole;
            RoleDetail = roleDetail;
            UserRoleDetail = userRoleDetail;            
            _anraConfiguration = anraConfiguration;
            Module = module;
            this.dbContext = dbContext;
        }

        [HttpGet("accesspermission/{moduleid}"), ActionName("accesspermission")]
        [Authorize]
        public AccessPermission GetAccessPermission(long moduleId)
        {
            var accessPermission = new AccessPermission();

            var userRole = UserRoleDetail.GetAll().Where(u => u.UserId == UserId && u.IsActive).Select(x => x.RoleId).ToArray();
            var permRole = RoleDetail.GetAll().Where(r => userRole.Contains(r.RoleId) && r.IsActive && r.ModuleId == moduleId);

            //to get highest value from all roles
            foreach (var rd in permRole)
            {
                accessPermission.View = rd.View || accessPermission.View;
                accessPermission.Delete = rd.Delete || accessPermission.Delete;
                accessPermission.Edit = rd.Edit || accessPermission.Edit;
                accessPermission.Add = rd.Add || accessPermission.Add;
            }           

            return accessPermission;
        }

        [HttpGet("getrole"), ActionName("getrole")]
        [Authorize]
        public string GetRole()
        {
            return AnraCommon.GetRole(UserEmail, dbContext);
        }

        [HttpGet("listing"), ActionName("listing")]
        [Authorize]
        public UserListing GetListing()
        {
            var listing = new UserListing();
            var users = UserRepository.GetAll(UserEmail).Select(p => p).ToList();
            var rolesLookup = new Dictionary<string, string>();
            foreach (var user in users)
            {
                //var roles = userManager.GetRolesAsync(user).Result.ToArray();
                var roleIds = UserRoleDetail.GetAll().Where(r => r.UserId == user.Id).Select(r => r.RoleId).ToArray();
                var roles = AnraRole.GetAll().Where(r => roleIds.Contains(r.RoleId)).Select(r => r.RoleName).ToArray();
                rolesLookup.Add(user.Id, string.Join(",", roles));
            }

            listing.Users = users.ToList().Adapt<List<User>, IEnumerable<UserListItem>>().ToList();

            foreach (var user in listing.Users)
            {
                user.RoleName = rolesLookup[user.Id];
                var org = OrganizationRepositoy.Find(user.OrganizationId);
                user.OrganizationName = org != null ? org.Name : "";
            }

            return listing;
        }

        [HttpPost("SaveUser"), ActionName("SaveUser")]
        [Authorize]
        public async Task<OperationResult> Save([FromBody]UserItem userItem)
        {
            var operationResult = new OperationResult { IsSuccess = false };

            try
            {
                if (!(userItem.UserId == Guid.Empty))
                {
                    var user = userManager.FindByNameAsync(userItem.UserName).Result;

                    if (user != null)
                    {  
                        bool updateUserEmail = user.Email != userItem.Email;

                        //Set Modified Date
                        user.DateModified = DateTime.UtcNow;
                        userItem.Adapt(user);

                        var result = await userManager.UpdateAsync(user);

                        if (updateUserEmail)
                        {
                            user.UserName = user.Email;
                            await userManager.UpdateNormalizedEmailAsync(user);
                            await userManager.UpdateNormalizedUserNameAsync(user);
                        }

                        if (result.Succeeded)
                        {
                            foreach (var ud in UserRoleDetail.GetAll().Where(r => r.UserId == user.Id).ToList())
                            {
                                UserRoleDetail.Remove(ud.UserRoleDetailId);
                            }

                            foreach (var role in userItem.SelectedRole)
                            {
                                UserRoleDetail.Add(new UserRoleDetail { RoleId = role.RoleId, UserId = user.Id, IsActive = true });
                            }

                            operationResult.IsSuccess = true;
                            operationResult.SuccessMessage = "User Updated Successfully.";
                        }
                        else
                        {
                            operationResult.ErrorMessage = string.Join(", ", result.Errors.Select(p => p.Description).ToArray());
                        }
                        
                    }
                    else
                    {
                        operationResult.ErrorMessage = "User not found!";
                    }
                }
                else
                {  
                        var newUser = userItem.Adapt<UserItem, User>();
                        newUser.Id = Guid.NewGuid().ToString();
                        newUser.UserName = userItem.Email;
                        newUser.AdminId = UserEmail;
                        
                        newUser.DateCreated = DateTime.UtcNow;

                        //get package id and organizatin id incase of non admin user
                        if (newUser.AdminId != null)
                        {
                            var adminuser = userManager.FindByEmailAsync(newUser.AdminId).Result;                            
                            newUser.OrganizationId = adminuser.OrganizationId;
                            newUser.AdminId = adminuser.Id;
                        }
                        else
                        {
                            //save organization and then associate it with user
                            if (!string.IsNullOrEmpty(userItem.OrganizationName))
                            {
                                var org = new Organization
                                {
                                    Name = userItem.OrganizationName
                                };
                                OrganizationRepositoy.Add(org);
                                newUser.OrganizationId = org.OrganizationId;
                            }
                        }
                        
                        var result = await userManager.CreateAsync(newUser, userItem.Password);
                        if (result.Succeeded)
                        {
                            //get package id and organizatin id incase of non admin user
                            if (string.IsNullOrEmpty(UserEmail))
                            {
                                UserRoleDetail.Add(new UserRoleDetail { RoleId = Convert.ToInt32(Roles.Admin), UserId = newUser.Id, IsActive = true });//admin role
                            }
                            else
                            {
                                foreach (var role in userItem.SelectedRole)
                                {
                                    UserRoleDetail.Add(new UserRoleDetail { RoleId = role.RoleId, UserId = newUser.Id, IsActive = true });
                                }
                            }

                            operationResult.IsSuccess = true;
                            operationResult.SuccessMessage = "User Created Successfully.";
                        }
                        else
                        {
                            operationResult.ErrorMessage = string.Join(", ", result.Errors.Select(p => p.Description).ToArray());
                        }
                }
            }
            catch (Exception ex)
            {
                operationResult.ErrorMessage = ex.Message;
            }

            return operationResult;
        }

        [HttpGet("{id}")]
        [Authorize]
        public UserItem Get(string id)
        {
            var user = UserRepository.Find(id);

            var userItem = user.Adapt<User, UserItem>();

            userItem.RoleLookup = AnraRole.GetAll().Where(x => x.RoleId != Convert.ToInt32(Roles.SuperAdmin)).Select(p => new ViewModel.Roles.RoleItem { RoleId = p.RoleId, RoleName = p.RoleName }).ToList();

            var roleIds = UserRoleDetail.GetAll().Where(r => r.UserId == user.Id).Select(r => r.RoleId).ToArray();
            var roles = AnraRole.GetAll().Where(r => roleIds.Contains(r.RoleId));

            userItem.RoleName = string.Join(", ", roles);
            userItem.RoleLookup.ForEach(x => x.IsSelected = roles.Where(r => r.RoleId == x.RoleId).Count() > 0);

            userItem.SelectedRole = roles.Adapt<IEnumerable<Role>, List<ViewModel.Roles.RoleItem>>();
            userItem.SelectedRole.ForEach(x => x.IsSelected = true);

            userItem.OrganizationLookup = OrganizationRepositoy.GetAll().Adapt<IEnumerable<Organization>, List<OrganizationListItem>>();

            if (userItem.OrganizationId > 0)
            {
                var organization = OrganizationRepositoy.Find(userItem.OrganizationId);
                if (organization != null)
                    userItem.OrganizationName = organization.Name;
            }

            return userItem;
        }

        [HttpDelete("{id}")]
        //[Authorize("Bearer")]
        public OperationResult Delete(string id)
        {
            var result = new OperationResult { IsSuccess = false };
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var user = UserRepository.Find(id);
                    //userManager.DeleteAsync(user);
                    user.IsDeleted = true;
                    user.IsActive = false;
                    UserRepository.Update(user);

                    result.IsSuccess = true;
                }
                else
                {
                    result.ErrorMessage = "An error occurred while deleting the user.";
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        [HttpGet("userDetail/{id}")]
        public UserDetailItem getUserDetail(string id)
        {
            var userDetailItem = new UserDetailItem();
            var userItem = new UserItem();

            var user = UserRepository.Find(id);

            if (user == null)
            {
                return new UserDetailItem();
            }

            userItem = user.Adapt<User, UserItem>();

            var roleIds = UserRoleDetail.GetAll().Where(r => r.UserId == user.Id).Select(r => r.RoleId).ToArray();
            var roles = AnraRole.GetAll().Where(r => roleIds.Contains(r.RoleId));

            userDetailItem.UserName = userItem.FirstName + userItem.LastName;
            userDetailItem.UserEmail = userItem.Email;
            userDetailItem.UserRoles = roles.Adapt<IEnumerable<Role>, List<ViewModel.Roles.RoleItem>>();
            userDetailItem.PhoneNumber = userItem.PhoneNumber != null ? userItem.PhoneNumber : "000000000";

            return userDetailItem;
        }

        [HttpPost("ChangePassword"), ActionName("ChangePassword")]
        public async Task<OperationResult> ChangePassword([FromBody]UserItem userItem)
        {
            var operationResult = new OperationResult { IsSuccess = false };

            try
            {
                if (!string.IsNullOrEmpty(userItem.UserName))
                {
                    var user = userManager.FindByNameAsync(userItem.UserName).Result;

                    if (user != null)
                    {
                        userItem.Adapt<UserItem, User>(user);
                        var result = await userManager.ChangePasswordAsync(user, userItem.CurrentPassword, userItem.NewPassword);
                        if (result.Succeeded)
                        {
                            operationResult.IsSuccess = true;
                        }
                        else
                        {
                            operationResult.ErrorMessage = string.Join(", ", result.Errors.Select(p => p.Description).ToArray());
                        }
                    }
                    else
                    {
                        operationResult.ErrorMessage = "User not found!";
                    }
                }
                else
                {
                    operationResult.ErrorMessage = "User not found!";
                }
            }
            catch (Exception ex)
            {
                operationResult.ErrorMessage = ex.Message;
            }

            return operationResult;
        }

        [HttpPost("ResetPassword"), ActionName("ResetPassword")]
        public async Task<OperationResult> ResetPassword([FromBody]UserItem userItem)
        {
            var operationResult = new OperationResult { IsSuccess = false };

            try
            {
                if (!string.IsNullOrEmpty(userItem.UserName))
                {
                    var user = userManager.FindByNameAsync(userItem.UserName).Result;

                    if (user != null)
                    {
                        userItem.Adapt<UserItem, User>(user);

                        var code = await userManager.GeneratePasswordResetTokenAsync(user);

                        var result = await userManager.ResetPasswordAsync(user, code, userItem.NewPassword);

                        if (result.Succeeded)
                        {
                            operationResult.IsSuccess = true;
                            operationResult.SuccessMessage = "Password Reset Successfully.";
                        }
                        else
                        {
                            operationResult.ErrorMessage = string.Join(", ", result.Errors.Select(p => p.Description).ToArray());
                        }
                    }
                    else
                    {
                        operationResult.ErrorMessage = "User not found!";
                    }
                }
                else
                {
                    operationResult.ErrorMessage = "User not found!";
                }
            }
            catch (Exception ex)
            {
                operationResult.ErrorMessage = ex.Message;
            }

            return operationResult;
        }

        [HttpGet("NewUser"), ActionName("NewUser")]
        [Authorize]
        public UserItem GetNewItem()
        {
            var newUserItem = new UserItem
            {
                RoleLookup = AnraRole.GetAll().Where(x => x.RoleId != Convert.ToInt32(Roles.SuperAdmin) && x.RoleId != Convert.ToInt32(Roles.Admin)).Select(p => new RoleItem { RoleId = p.RoleId, RoleName = p.RoleName }).ToList(),
                OrganizationLookup = OrganizationRepositoy.GetAll().Adapt<IEnumerable<Organization>, List<OrganizationListItem>>(),                
                OrganizationId = this.OrganizationId
            };

            if (newUserItem.OrganizationId > 0)
            {
                newUserItem.OrganizationName = OrganizationRepositoy.Find(newUserItem.OrganizationId).Adapt<Organization, OrganizationItem>().Name;
            }

            return newUserItem;
        }
    }
}