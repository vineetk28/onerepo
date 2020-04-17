using MantiScanServices.Model;
using MantiScanServices.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MantiScanServices.Common.Notification
{
    public class SendMailLogic
    {
        private readonly ILogger<SendMailLogic> _logger;
        private readonly IRepository<Model.Users.User> _userRepository;
        private readonly SendNotification _sendNotification;
        private readonly AnraConfiguration _configuration;

        public SendMailLogic(ILogger<SendMailLogic> logger,
            IRepository<Model.Users.User> UserRepository,
            SendNotification sendNotification,
            AnraConfiguration configuration)
        {
            _logger = logger;
            _userRepository = UserRepository;
            _sendNotification = sendNotification;
            _configuration = configuration;
        }

        public async void BulkSendMailAsync(string[] userIds)
        {
            #if DEBUG
                _logger.LogInformation("BulkClose {0}", userIds);
            #endif

            foreach (var userId in userIds)
            {
                await ToSendMailAsync(userId);
            }
        }

        public async Task<ObjectResult> ToSendMailAsync(string userId)
        {
            var model = _userRepository.Find(userId);

            if (model == null || model.IsDeleted)
                return new ObjectResult(new ResponseMsg { HttpStatusCode = (int)HttpStatusCode.NotFound, Message = "User not found." });

            #if DEBUG
                   _logger.LogInformation("SendMailLogic ::: ToSendMailAsync :: Operation : {0}", model.Id);
            #endif

            StringBuilder SbBody = new StringBuilder();
            SbBody.Append("Dear Admin, <br/>");
            SbBody.Append("Your account is about to expire. Kindly update it. <br/><br/>");                       
            SbBody.Append("<br/><br/><br/>");
            SbBody.Append("<b>Regards,</b><br/>");
            SbBody.Append("ANRA Notification Manager");
            SbBody.Append("<hr/>");

            _sendNotification.SendEmail(model.Email, SbBody.ToString(), "Alert from anra technologies");


            return new ObjectResult(new ResponseMsg { HttpStatusCode = (int)HttpStatusCode.OK, Message = $"E-Mail is sent to user ({model.FirstName}-{model.LastName}) with id ({model.Id})." });
        }
    }
}
