using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MantiScanServices.Common;
using MantiScanServices.Common.Notification;
using MantiScanServices.ViewModel;
using MantiScanServices.ViewModel.Operation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MantiScanServices.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CommunicationController : BaseController
    {
        private readonly SendNotification sendNotification;

        public CommunicationController(SendNotification _sendNotification)
        {
            sendNotification = _sendNotification;
        }

        /// <summary>
        /// send  mail to user.
        /// </summary>
        [HttpPost]
        [Route(@"save")]
        [SwaggerOperation(nameof(PostEmail))]
        public virtual IActionResult PostEmail([FromBody]EmailTestViewModel emailTestViewModel)
        {
            //Send Email Notification
            var emailBody = "Hello, how r u !";

            var mailConfiguration = new MailConfiguration()
            {
                Body = emailBody,
                Subject = "test mail",
                ReceiversEmail = "ravi_ibm786@yahoo.com"
            };

            try
            {
                sendNotification.SendEmail(mailConfiguration);

                return new ObjectResult(new OperationResult { IsSuccess = true, ErrorMessage = "Send successfully" });
            }
            catch (Exception ex)
            {
                return new ObjectResult(new OperationResult { IsSuccess = false, ErrorMessage = ex.Message });
            }
        }
    }
}