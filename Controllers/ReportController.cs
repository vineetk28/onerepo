using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MantiScanServices.DataProvider;
using MantiScanServices.Model;
using MantiScanServices.Model.Organizations;
using MantiScanServices.Model.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MantiScanServices.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ReportController : BaseController
    {
        private readonly MantiDbContext dbContext;
        public IRepository<User> UserRepository { get; set; }
        public IRepository<IdentityRole> RoleRepository { get; set; }
        public IRepository<Organization> OrganizationRepositoy { get; set; }

        public ReportController([FromServices] IRepository<User> userRepository,
              [FromServices] IRepository<IdentityRole> roleRepositoy,
              [FromServices] IRepository<Organization> organizationRepositoy,
              MantiDbContext dbContext)
        {
            UserRepository = userRepository;
            RoleRepository = roleRepositoy;
            OrganizationRepositoy = organizationRepositoy;
            this.dbContext = dbContext;
        }

       

    }
}