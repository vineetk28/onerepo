using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantiScanServices.Common
{
    public class AnraConfiguration
    {
        public string Product;
        public string Environment;

        public string DefaultOrganization;// = "ANRA Technologies";
        public string DefaultSuperAdmin; //= "admin@flyanra.com";
        public string DefaultAdmin;// = "sales@flyanra.com";

        /// <summary>
        /// Email and Text settings
        /// </summary>
        public string EmailFrom;
        public string EmailPassword;
        public string SmtpHost;
        public string SendersEmail;
        public string SendersEmailPassword;

        public static void Configure(IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddSingleton(new AnraConfiguration {

                Product = configuration.GetSection("ProductSettings").GetValue<string>("Product"),
                Environment = configuration.GetSection("ProductSettings").GetValue<string>("Environment"),

                EmailFrom = configuration.GetSection("EmailSettings").GetValue<string>("EmailFrom"),
                EmailPassword = configuration.GetSection("EmailSettings").GetValue<string>("EmailPassword"),
                DefaultSuperAdmin = configuration.GetSection("ProductSettings").GetValue<string>("DefaultSuperAdmin"),

                SendersEmail = configuration.GetSection("EmailTextSettings").GetValue<string>("SendersEmail"),
                SendersEmailPassword = configuration.GetSection("EmailTextSettings").GetValue<string>("SendersEmailPassword"),
                SmtpHost = configuration.GetSection("EmailTextSettings").GetValue<string>("SmtpHost")

            });
        }
    }
}
