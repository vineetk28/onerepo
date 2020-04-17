using FluentScheduler;
using MantiScanServices.Common.Notification;
using MantiScanServices.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantiScanServices.BackgroundJobs
{
    public class NotifyMailJob : IJob
    {
        private readonly ILogger<string> _logger;
        private readonly IServiceProvider serviceProvider;

        public NotifyMailJob(IServiceProvider serviceProvider, ILogger<string> logger)
        {
            this.serviceProvider = serviceProvider;
            this._logger = logger;
        }

        public void Execute()
        {
            NotifyUserViaMail();
        }

        private void NotifyUserViaMail()
        {
            try
            {
                #if DEBUG
                    _logger.LogDebug("NotifyMailJob :: NotifyUserViaMail");
                #endif

                var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
                using (serviceScope)
                {
                    var userRepository = serviceScope.ServiceProvider.GetService<IRepository<Model.Users.User>>();
                    var usersId = userRepository.GetAll()
                        .Where(u=>u.IsActive && u.IsDeleted == false)
                        .Select(p=>p.Id)
                        .Distinct()
                        .ToArray();

                    var sendMailLogic = serviceScope.ServiceProvider.GetService<SendMailLogic>();
                    sendMailLogic.BulkSendMailAsync(usersId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("NotifyMailJob ::: Exception : {0}", ex);
            }
        }
    }
}
