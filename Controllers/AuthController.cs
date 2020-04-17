using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MantiScanServices.Model;
using MantiScanServices.Model.Organizations;
using MantiScanServices.Model.Roles;
using MantiScanServices.Model.Users;
using MantiScanServices.TokenProvider;
using MantiScanServices.ViewModel.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MantiScanServices.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private SignInManager<User> signInManager;
        private UserManager<User> userManager;
        private readonly TokenProviderOptions tokenOptions;
        private readonly ILogger _logger;
        private IRepository<User> userRepo;
        private IRepository<Organization> orgRepo;
        private IRepository<UserRoleDetail> userRoleDetail;
        private IRepository<Role> anraRole;

        public AuthController([FromServices] SignInManager<User> signInManager,
           [FromServices] UserManager<User> userManager,
           [FromServices] IRepository<User> userRepo,
           [FromServices] IRepository<Organization> orgRepo,
           [FromServices] IRepository<UserRoleDetail> userRoleDetail,
           [FromServices] IRepository<Role> anraRole,
           [FromServices] TokenProviderOptions tokenOptions,
           [FromServices] ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("AuthController");
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.tokenOptions = tokenOptions;
            this.userRepo = userRepo;
            this.orgRepo = orgRepo;
            this.userRoleDetail = userRoleDetail;
            this.anraRole = anraRole;
        }

        /// <summary>
        /// Authenticate a user and issue a token if valid user
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AuthResult> Post([FromBody] AuthRequest req)
        {
            var result = await signInManager.PasswordSignInAsync(req.Email, req.Password, true, false);
            if (result.Succeeded)
            {   
                var user = userManager.FindByEmailAsync(req.Email).Result;
                var roleIds = userRoleDetail.GetAll().Where(r => r.UserId == user.Id).Select(r => r.RoleId).ToArray();
                var roles = anraRole.GetAll().Where(r => roleIds.Contains(r.RoleId)).Select(x => x.RoleName).ToList();
                var userRoles = roles.Count > 0 ? string.Join(", ", roles) : string.Empty;
                var org = orgRepo.Find(user.OrganizationId);
                // var unitId = org.UnitId;
                var token = GetToken(req.Email, roles);

                #if DEBUG
                        _logger.LogInformation($"Token Created.");
                #endif

                return new AuthResult
                {
                    IsAuthenticated = true,
                    UserId = user.Id,
                    UserEmail = req.Email,
                    UserName = string.Join(" ", user.FirstName, user.LastName),
                    Roles = userRoles,
                    Token = token.Result,
                    TokenExpires = DateTime.UtcNow.Add(tokenOptions.Expiration),
                    OrgLogo = org.LogoFile                    
                };
            }
            else
            {
                // Response.StatusCode = 401;

                #if DEBUG
                    _logger.LogError($"An error occured.");
                #endif

                return new AuthResult { IsAuthenticated = false, Message = "Invalid Email or Password" };
            }
        }

        private async Task<string> GetToken(string userEmail, List<string> userRoles)
        {
            var handler = new JwtSecurityTokenHandler();
            var userItem = userManager.FindByEmailAsync(userEmail).Result;
            

            var claims = new List<Claim>{
                new Claim("UserId", userItem.Id),
                new Claim("EmailId", userItem.Email),
                new Claim("OrganizationId", userItem.OrganizationId.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, userEmail),
                new Claim(JwtRegisteredClaimNames.Jti, await tokenOptions.NonceGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64)
           };

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var securityToken = new JwtSecurityToken(
                tokenOptions.Issuer,
                tokenOptions.Audience,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.Add(tokenOptions.Expiration),
                tokenOptions.SigningCredentials);

            return handler.WriteToken(securityToken);
        }

        public static long ToUnixEpochDate(DateTime date) => new DateTimeOffset(date).ToUniversalTime().ToUnixTimeSeconds();

        /// <summary>
        /// Used to Log out user
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("logout"), ActionName("logout")]
        public void Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                signInManager.SignOutAsync();
            }
        }

        //[HttpGet("CurrentUser"), ActionName("CurrentUser"), Authorize("Bearer")]
        //public List<string> GetCurrentUser()
        //{
        //    var roles = ((ClaimsIdentity)HttpContext.User.Identity).Claims
        //        .Where(c => c.Type == ClaimTypes.Role)
        //        .Select(c => c.Value);

        //    var temp = HttpContext.User.HasClaim(ClaimTypes.Role, "MANAGE_DRONES");
        //    return roles.ToList();
        //}

    }
}