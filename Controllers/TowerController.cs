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
using System.Net;
using MantiScanServices.Model.Master;
using MantiScanServices.ViewModel.Master;
using MantiScanServices.ViewModel.Operation;
using Swashbuckle.AspNetCore.Annotations;

namespace MantiScanServices.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TowerController : BaseController
    {
        private readonly MantiDbContext dbContext;
        public IRepository<Tower> TowerRepository { get; set; }
        public IRepository<User> UserRepository { get; set; }
        public IRepository<IdentityRole> RoleRepository { get; set; }
        public IRepository<Organization> OrganizationRepositoy { get; set; }

        public TowerController([FromServices] IRepository<User> userRepository,
              [FromServices] IRepository<IdentityRole> roleRepositoy,
              [FromServices] IRepository<Organization> organizationRepositoy,
              [FromServices]IRepository<Tower> towerRepository,
              MantiDbContext dbContext)
        {
            UserRepository = userRepository;
            RoleRepository = roleRepositoy;
            OrganizationRepositoy = organizationRepositoy;
            TowerRepository = towerRepository;
            this.dbContext = dbContext;
        }

        /// <summary>
		/// Returns tower list for the authenticated user
		/// </summary>		
		[HttpGet]
        [Authorize]
        [Route(@"listing")]
        [SwaggerOperation(nameof(GetAllTowers)), SwaggerResponse(200, type: typeof(List<TowerViewModel>))]
        public List<TowerViewModel> GetAllTowers()
        {
            var towers = TowerRepository.GetAll(UserEmail).Adapt<IEnumerable<Tower>, List<TowerViewModel>>();
            return towers;
        }

        /// <summary>
		/// Request information regarding Specific tower
		/// </summary>
		/// <remarks>Allows querying for tower data.</remarks>
		/// <param name="TowerId">Id</param>
		/// <response code="200">Successful data request. Response includes requested data.</response>
		/// <response code="400">Bad request. Typically validation error. Fix your request and retry.</response>
		/// <response code="429">Too many recent requests from you. Wait to make further queries.</response>
        [HttpGet]
        [Authorize]
        [Route(@"{TowerId}")]
        public virtual IActionResult GetTowerById([FromRoute]long TowerId)
        {
            var twrModel = TowerRepository.Find(TowerId);
            var twrVM = new TowerViewModel();

            if (twrModel != null)
            {
                twrVM = twrModel.Adapt<TowerViewModel>();
                return new ObjectResult(twrVM);
            }
            else
            {
                return new ObjectResult(new ResponseMsg { HttpStatusCode = (int)HttpStatusCode.BadRequest, Message = "Tower does not exist" });
            }
        }

        /// <summary>
		/// Delete Specific Tower
		/// </summary>
		/// <remarks>Delete Specific Tower</remarks>
		/// <param name="TowerId">Id</param>
		/// <response code="200">Successful data request. Response includes requested data.</response>
		/// <response code="400">Bad request. Typically validation error. Fix your request and retry.</response>
		/// <response code="429">Too many recent requests from you. Wait to make further queries.</response>
        [HttpDelete]
        [Authorize]
        [Route(@"{TowerId}")]
        public virtual IActionResult DeleteReport([FromRoute]long TowerId)
        {
            var tower = TowerRepository.Find(TowerId);

            if (tower != null)
            {
                TowerRepository.Remove(tower.TowerId);

                return new ObjectResult(new ResponseMsg { HttpStatusCode = (int)HttpStatusCode.OK, Message = "Tower deleted successfully." });
            }
            else
            {
                return new ObjectResult(new ResponseMsg { HttpStatusCode = (int)HttpStatusCode.OK, Message = "Not deleted.tower does not exist" });
            }
        }

        /// <summary>
        /// Create New tower.
        /// </summary>
        [HttpPost]
        [Authorize]
        [Route(@"save")]       
        public virtual IActionResult PostTower([FromBody]TowerViewModel towerViewModel)
        {
            try
            {
                towerViewModel.UserId = UserId;
                var existingTower = TowerRepository.Find(towerViewModel.TowerId);

                if (existingTower != null)
                {
                    return new ObjectResult(new OperationResult { IsSuccess = false, ErrorMessage = "Tower is already exist" });
                }

                var towerModel = towerViewModel.Adapt<TowerViewModel, Tower>();
                TowerRepository.Add(towerModel);

                return new ObjectResult(new OperationResult { IsSuccess = true, ErrorMessage = "Created Successfully" });
            }
            catch (Exception ex)
            {
                return new ObjectResult(new OperationResult { IsSuccess = false, ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// Update existing country.
        /// </summary>
        [HttpPut]
        [Authorize]
        [Route(@"update")]
        public virtual IActionResult PutTower([FromBody]TowerViewModel towerViewModel)
        {
            try
            {
                towerViewModel.UserId = UserId;
                var existingTower = TowerRepository.Find(towerViewModel.TowerId);

                if (existingTower != null)
                {
                    if (towerViewModel.TowerId > 0)
                    {
                        towerViewModel.Adapt(existingTower);
                        TowerRepository.Update(existingTower);

                        return new ObjectResult(new OperationResult { IsSuccess = true, ErrorMessage = "Tower Updated Successfully" });
                    }
                    else
                    {
                        return new ObjectResult(new OperationResult { IsSuccess = false, ErrorMessage = "Not Updated" });
                    }
                }
                else
                {
                    return new ObjectResult(new OperationResult { IsSuccess = false, ErrorMessage = "tower does not exist" });
                }
            }
            catch (Exception ex)
            {
                return new ObjectResult(new OperationResult { IsSuccess = false, ErrorMessage = ex.Message });
            }
        }

    }
}