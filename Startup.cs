using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MantiScanServices.Common;
using MantiScanServices.Common.Notification;
using MantiScanServices.DataProvider;
using MantiScanServices.Model;
using MantiScanServices.Model.Incidents;
using MantiScanServices.Model.Organizations;
using MantiScanServices.Model.Modules;
using MantiScanServices.Model.Roles;
using MantiScanServices.Model.Users;
using MantiScanServices.TokenProvider;
using MantiScanServices.ViewModel.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
using MantiScanServices.Model.Master;
using MantiScanServices.Model.Plateforms;
using MantiScanServices.Model.OilSpillReport;

namespace MantiScanServices
{
    public class Startup
    {
        public IConfiguration Configuration_ { get; set; }
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
           // Configuration = configuration;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional:false,reloadOnChange:true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddApplicationInsightsSettings(developerMode: env.IsDevelopment());

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.BufferBodyLengthLimit = Int64.MaxValue;
                x.MultipartBodyLengthLimit = Int64.MaxValue;
            });

            var sqlConnectionString = Configuration.GetConnectionString("PostgreSqlProviderPath");

            services.AddDbContext<MantiDbContext>(options =>
                options.UseNpgsql(
                    sqlConnectionString,
                    b => b.MigrationsAssembly(nameof(MantiScanServices))
                )
            );
            ConfigureCommonObjects(services);
            services.AddCors();

            services.AddIdentity<User, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength = 6;
                config.Password.RequireUppercase = false;
                config.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<MantiDbContext>()
            .AddDefaultTokenProviders();

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Audience = "AnraUsers";
                options.ClaimsIssuer = "AnraTechnologies";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = "AnraTechnologies",
                    IssuerSigningKey = new RsaSecurityKey(RSAKeyUtils.GetKey())
                };
            });

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSerilog();
            });

            services.AddMvc()
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Bearer",
                    authBuilder =>
                    {
                        authBuilder.RequireRole("Administrators");
                    });
            });

            AddCustomDependencies(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "MantiScan API", Version = "MantiScan Services v1.0.0" });
                c.DescribeAllEnumsAsStrings();
                var basePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, Assembly.GetExecutingAssembly().GetName().Name + ".xml");
                c.IncludeXmlComments(basePath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {  
            app.UseSwagger();
            app.UseCors(b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseMvc();

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<MantiDbContext>().Database.Migrate();
            }

            MantiScanContextSeedData.EnsureSeedDataAsync(app.ApplicationServices).ConfigureAwait(false);

            appLifetime.ApplicationStopped.Register(Log.CloseAndFlush);

            var path = Configuration.GetSection("ProductSettings").GetValue<string>("MantiScanServiceUrl");
            app.UseSwaggerUI(s => s.SwaggerEndpoint(path + "swagger/v1/swagger.json", "MantiScan API"));
        }

        private void ConfigureCommonObjects(IServiceCollection services)
        {
            AnraConfiguration.Configure(services, Configuration);

            //services.AddSingleton(new SendNotification(services.BuildServiceProvider().GetRequiredService<AnraConfiguration>()));

            services.AddSingleton(new TokenProviderOptions
            {
                Audience = "AnraUsers",
                Issuer = "AnraTechnologies",
                SigningCredentials = new SigningCredentials(new RsaSecurityKey(RSAKeyUtils.GetKey()), SecurityAlgorithms.RsaSha256Signature),
                Expiration = TimeSpan.FromMinutes(Configuration.GetSection("LoginSettings").GetValue<double>("Expiration"))
            });
        }

        private static void AddCustomDependencies(IServiceCollection services)
        {
            FluentScheduler.JobManager.Initialize();

            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<IdentityRole>, RoleRepository>();
            services.AddScoped<IRepository<Organization>, OrganizationRepository>();
            services.AddScoped<IRepository<Incident>, IncidentRepository>();
            services.AddScoped<IRepository<Model.Modules.Module>, ModuleRepository>();
            services.AddScoped<IRepository<RoleDetail>, RoleDetailRepository>();
            services.AddScoped<IRepository<UserRoleDetail>, UserRoleDetailRepository>();
            services.AddScoped<IRepository<Role>, AnraRoleRepository>();
            services.AddScoped<IRepository<Tower>, TowerRepository>();
            services.AddScoped<IRepository<PlateForm>, PlateFormRepository>();
            services.AddScoped<IRepository<OilSpillReport>, OilSpillReportRepository>();

            services.AddScoped<MantiScanContextSeedData, MantiScanContextSeedData>();
            services.AddSingleton<DatabaseFunctions>();
            services.AddSingleton<SendNotification>();
        }
    }
}
