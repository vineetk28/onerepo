using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MantiScanServices.DataProvider;
using MantiScanServices.Model;
using MantiScanServices.Model.Organizations;
using MantiScanServices.Model.Users;
using MantiScanServices.ViewModel;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MantiScanServices.Model.Incidents;
using MantiScanServices.ViewModel.Incident;
using System.Net;

namespace MantiScanServices.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class IncidentReportController : BaseController
    {
        private readonly MantiDbContext dbContext;
        public IRepository<Incident> IncidentRepository { get; set; }
        public IRepository<User> UserRepository { get; set; }
        public IRepository<IdentityRole> RoleRepository { get; set; }
        public IRepository<Organization> OrganizationRepositoy { get; set; }

        public IncidentReportController([FromServices] IRepository<User> userRepository,
              [FromServices] IRepository<IdentityRole> roleRepositoy,
              [FromServices] IRepository<Organization> organizationRepositoy,
              [FromServices]IRepository<Incident> incidentRepository,
              MantiDbContext dbContext)
        {
            UserRepository = userRepository;
            RoleRepository = roleRepositoy;
            OrganizationRepositoy = organizationRepositoy;
            IncidentRepository = incidentRepository;
            this.dbContext = dbContext;
        }

        /// <summary>
		/// Returns IncidentReport list for the authenticated user
		/// </summary>		
		[HttpGet]
        [Authorize]
        [Route(@"listing")]
        public List<IncidentViewModel> GetAllIncidentReport()
        {
            var reports = IncidentRepository.GetAll(UserEmail).Adapt<IEnumerable<Incident>, List<IncidentViewModel>>();
            return reports;
        }

        /// <summary>
		/// Request information regarding Specific Report
		/// </summary>
		/// <remarks>Allows querying for Report data.</remarks>
		/// <param name="id">Id</param>
		/// <response code="200">Successful data request. Response includes requested data.</response>
		/// <response code="400">Bad request. Typically validation error. Fix your request and retry.</response>
		/// <response code="429">Too many recent requests from you. Wait to make further queries.</response>
        [HttpGet]
        [Authorize]
        [Route(@"{id}")]
        public virtual IActionResult GetIncidentReportById([FromRoute]long id)
        {
            var rptModel = IncidentRepository.Find(id);
            var report = new IncidentViewModel();

            if (rptModel != null)
            {
                report = rptModel.Adapt<IncidentViewModel>();
                return new ObjectResult(report);
            }
            else
            {
                return new ObjectResult(new ResponseMsg { HttpStatusCode = (int)HttpStatusCode.BadRequest, Message = "Report does not exist" });
            }
        }

        /// <summary>
		/// Returns IncidentReport list based on tower id
		/// </summary>		
        /// /// <param name="TowerId">Id</param>
		[HttpGet]
        [Authorize]
        [Route(@"{TowerId}")]
        public List<IncidentViewModel> GetIncidentReportByTowerId([FromRoute] string TowerId)
        {
            var reports = IncidentRepository.GetAll(UserEmail);
            var reportsByTowerId = reports.Where(r => r.TowerId == TowerId).Adapt<IEnumerable<Incident>, List<IncidentViewModel>>();
            return reportsByTowerId;
        }

        /// <summary>
		/// Delete Specific Report
		/// </summary>
		/// <remarks>Delete Specific Report</remarks>
		/// <param name="id">Id</param>
		/// <response code="200">Successful data request. Response includes requested data.</response>
		/// <response code="400">Bad request. Typically validation error. Fix your request and retry.</response>
		/// <response code="429">Too many recent requests from you. Wait to make further queries.</response>
        [HttpDelete]
        [Authorize]
        [Route(@"{id}")]
        public virtual IActionResult DeleteReport([FromRoute]long id)
        {
            var reports = IncidentRepository.Find(id);

            if (reports != null)
            {
                IncidentRepository.Remove(reports.IncidentId);
                return new ObjectResult(new ResponseMsg { HttpStatusCode = (int)HttpStatusCode.OK, Message = "Report deleted successfully." });
            }
            else
            {
                return new ObjectResult(new ResponseMsg { HttpStatusCode = (int)HttpStatusCode.OK, Message = "Not deleted.Report does not exist" });
            }
        }
    }
}