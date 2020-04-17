using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MantiScanServices.DataProvider;
using MantiScanServices.Model;
using MantiScanServices.Model.OilSpillReport;
using MantiScanServices.Model.Organizations;
using MantiScanServices.Model.Users;
using MantiScanServices.ViewModel;
using MantiScanServices.ViewModel.OilSpillReport;
using MantiScanServices.ViewModel.Operation;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MantiScanServices.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class OilSpillReportController : BaseController
    {
        private readonly MantiDbContext dbContext;
        public IRepository<OilSpillReport> OilSpillReportRepository { get; set; }
        public IRepository<User> UserRepository { get; set; }
        public IRepository<IdentityRole> RoleRepository { get; set; }
        public IRepository<Organization> OrganizationRepositoy { get; set; }

        public OilSpillReportController([FromServices] IRepository<User> userRepository,
              [FromServices] IRepository<IdentityRole> roleRepositoy,
              [FromServices] IRepository<Organization> organizationRepositoy,
              [FromServices]IRepository<OilSpillReport> oilSpillReportRepository,
              MantiDbContext dbContext)
        {
            UserRepository = userRepository;
            RoleRepository = roleRepositoy;
            OrganizationRepositoy = organizationRepositoy;
            OilSpillReportRepository = oilSpillReportRepository;
            this.dbContext = dbContext;
        }

        /// <summary>
		/// Returns OilSpillReport list for the authenticated user
		/// </summary>		
		[HttpGet]
        [Authorize]
        [Route(@"/OilSpillReport/listing")]        
        public List<OilSpillReportView> GetAllOilSpillReport()
        {
            var reports = OilSpillReportRepository.GetAll(UserEmail).Adapt<IEnumerable<OilSpillReport>, List<OilSpillReportView>>();
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
        [Route(@"/OilSpillReport/{id}")]        
        public virtual IActionResult GetOilSpillReportById([FromRoute]long id)
        {
            var rptModel = OilSpillReportRepository.Find(id);
            var report = new OilSpillReportView();
            if (rptModel != null)
            {
                report = rptModel.Adapt<OilSpillReportView>();
                return new ObjectResult(report);
            }
            else
            {                
                return new ObjectResult(new ResponseMsg { HttpStatusCode = (int)HttpStatusCode.BadRequest, Message = "Report does not exist" });
            }
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
        [Route(@"/OilSpillReport/{id}")]        
        public virtual IActionResult DeleteReport([FromRoute]long id)
        {
            var reports = OilSpillReportRepository.Find(id);

            if (reports != null)
            {
                OilSpillReportRepository.Remove(reports.OilSpillReportId);
                return new ObjectResult(new ResponseMsg { HttpStatusCode = (int)HttpStatusCode.OK, Message = "Report deleted successfully." });
            }
            else {
                return new ObjectResult(new ResponseMsg { HttpStatusCode = (int)HttpStatusCode.OK, Message = "Not deleted.Report does not exist" });
            }
        }

        /// <summary>
        /// create new Oil Spill Report or Update existing Oil Spill Report datail
        /// </summary>
        [HttpPost]
        [Authorize]
        [Route(@"/OilSpillReport")]
        public OperationResult SaveUpdateOilSpillReport([FromBody]OilSpillReportView oilSpillReportView)
        {
            var result = new OperationResult { IsSuccess = false };

            oilSpillReportView.UserId = UserId;
            if (oilSpillReportView.OrganizationId == 0)
                oilSpillReportView.OrganizationId = OrganizationId;

            try
            {
                if (oilSpillReportView.OilSpillReportId > 0)
                {
                    var existingReport = OilSpillReportRepository.Find(oilSpillReportView.OilSpillReportId);
                    oilSpillReportView.Adapt<OilSpillReportView, OilSpillReport>(existingReport);
                    OilSpillReportRepository.Update(existingReport);
                }
                else
                {
                    var OilSpillModel = oilSpillReportView.Adapt<OilSpillReportView, OilSpillReport>();
                    OilSpillReportRepository.Add(OilSpillModel);
                }

                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
    }
}