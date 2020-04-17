using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MantiScanServices.DataProvider;
using MantiScanServices.Model;
using MantiScanServices.Model.Organizations;
using MantiScanServices.Model.Plateforms;
using MantiScanServices.Model.Users;
using MantiScanServices.ViewModel;
using MantiScanServices.ViewModel.Operation;
using MantiScanServices.ViewModel.PlateForm;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MantiScanServices.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PlateFormController : BaseController
    {
        private readonly MantiDbContext dbContext;        
        public IRepository<User> UserRepository { get; set; }
        public IRepository<IdentityRole> RoleRepository { get; set; }
        public IRepository<Organization> OrganizationRepositoy { get; set; }
        public IRepository<PlateForm> PlateFormRepositoy { get; set; }

        public PlateFormController(IRepository<User> userRepository,
              [FromServices] IRepository<IdentityRole> roleRepositoy,
              [FromServices] IRepository<PlateForm> plateFormRepositoy,
              [FromServices] IRepository<Organization> organizationRepositoy,              
              MantiDbContext dbContext)
        {
            UserRepository = userRepository;
            RoleRepository = roleRepositoy;
            OrganizationRepositoy = organizationRepositoy;
            PlateFormRepositoy = plateFormRepositoy;
            this.dbContext = dbContext;
        }

        /// <summary>
		/// Returns Plateform list of specific organization
		/// </summary>
        [HttpGet]
        [Authorize]
        [Route(@"/PlateForm/listing")]
        public List<PlateFormViewModel> GetAllPlateform()
        {
            var reports = PlateFormRepositoy.GetAll(UserEmail).Adapt<IEnumerable<PlateForm>, List<PlateFormViewModel>>();
            return reports;
        }

        /// <summary>
        /// Request information regarding specific plateform
        /// </summary>
        /// <remarks>Allows querying for plateform data.</remarks>
        /// <param name="id">Id</param>
        /// <response code="200">Successful data request. Response includes requested data.</response>
        /// <response code="400">Bad request. Typically validation error. Fix your request and retry.</response>
        /// <response code="429">Too many recent requests from you. Wait to make further queries.</response>
        [HttpGet]
        [Authorize]
        [Route(@"/PlateForm/{id}")]
        public virtual IActionResult GetPlateFormById([FromRoute]long id)
        {   
                var rptModel = PlateFormRepositoy.Find(id);
                var report = new PlateFormViewModel();
                if (rptModel != null)
                {
                    report = rptModel.Adapt<PlateFormViewModel>();
                    return new ObjectResult(report);
                }
                else
                {
                    return new ObjectResult(new ResponseMsg { HttpStatusCode = (int)HttpStatusCode.BadRequest, Message = "Plateform does not exist" });
                }
        }

        /// <summary>
		/// Delete Specific Plateform
		/// </summary>
		/// <remarks>Delete Specific Plateform</remarks>
		/// <param name="id">Id</param>
		/// <response code="200">Successful data request. Response includes requested data.</response>
		/// <response code="400">Bad request. Typically validation error. Fix your request and retry.</response>
		/// <response code="429">Too many recent requests from you. Wait to make further queries.</response>
        [HttpDelete]
        [Authorize]
        [Route(@"/PlateForm/{id}")]
        public virtual IActionResult DeleteReport([FromRoute]long id)
        {
            var reports = PlateFormRepositoy.Find(id);

            if (reports != null)
            {
                PlateFormRepositoy.Remove(reports.PlateFormId);
                return new ObjectResult(new ResponseMsg { HttpStatusCode = (int)HttpStatusCode.OK, Message = "PlateForm deleted successfully." });
            }
            else
            {
                return new ObjectResult(new ResponseMsg { HttpStatusCode = (int)HttpStatusCode.OK, Message = "Not deleted.PlateForm does not exist" });
            }
        }

        /// <summary>
        /// create new Plateform or Update existing platform(oil rig) datail
        /// </summary>
        [HttpPost]
        [Authorize]
        [Route(@"/PlateForm")]
        public OperationResult SaveUpdatePlateForm([FromBody]PlateFormViewModel plateFormViewModel)
        {
            var result = new OperationResult { IsSuccess = false };
            plateFormViewModel.UserId = UserId;

            if (plateFormViewModel.OrganizationId == 0)
                plateFormViewModel.OrganizationId = OrganizationId;

            try
            {
                if (plateFormViewModel.PlateFormId > 0)
                {
                    var existingPlateForm = PlateFormRepositoy.Find(plateFormViewModel.PlateFormId);
                    plateFormViewModel.Adapt<PlateFormViewModel, PlateForm>(existingPlateForm);
                    PlateFormRepositoy.Update(existingPlateForm);
                }
                else
                {
                    var plateFormModel = plateFormViewModel.Adapt<PlateFormViewModel, PlateForm>();
                    PlateFormRepositoy.Add(plateFormModel);
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