using MantiScanServices.Common;
using MantiScanServices.Model.Users;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MantiScanServices.Model.Roles;
using MantiScanServices.Model.Modules;
using MantiScanServices.Model.Organizations;

namespace MantiScanServices.DataProvider
{
    public class MantiScanContextSeedData
    {
        private static IServiceScope serviceScope;
        private static MantiDbContext context { get { return serviceScope.ServiceProvider.GetService<MantiDbContext>(); } }

        public static async Task EnsureSeedDataAsync(IServiceProvider serviceProvider)
        {
            serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using (serviceScope)
            {
                var db = serviceScope.ServiceProvider.GetService<MantiDbContext>();
                await db.Database.EnsureCreatedAsync();

                var signInManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<Microsoft.AspNetCore.Identity.IdentityRole>>();
                var anraConfiguration = serviceScope.ServiceProvider.GetService<AnraConfiguration>();
                await SeedDatabaseAsync(signInManager, roleManager, anraConfiguration);
            }
        }

        private static async Task SeedDatabaseAsync(UserManager<User> signInManager, RoleManager<IdentityRole> roleManager, AnraConfiguration anraConfiguration)
        {
            UserRoleDetail urd;

            #region "Roles Creation"

            var roles = new[] { "SuperAdmin", "Administrator","SystemUser","Other" };//"Pilot", "Payload Operator", "Visual Observer",
            if (!(context.MantiScanRole.Any(u => roles.Contains(u.RoleName))))
            {
                foreach (var roleName in roles)
                {
                    var role = new Role { RoleName = roleName, IsActive = true, };
                    context.MantiScanRole.Add(role);
                }
                context.SaveChanges();
            }

            #endregion "Roles Creation"

            #region "Module Creation"

            if (!(context.Modules.Any(u => u.ModuleName == nameof(User))))
            {
                var lstModule = new List<Module>
                {
                    new Module { ModuleId = 1, ModuleName = "User", IsActive = true },
                    new Module { ModuleId = 2, ModuleName = "Organization", IsActive = true },
                    new Module { ModuleId = 4, ModuleName = "Plateform", IsActive = true },
                    new Module { ModuleId = 3, ModuleName = "Incident", IsActive = true },
                    new Module { ModuleId = 5, ModuleName = "Report", IsActive = true }
                };
                context.Modules.AddRange(lstModule);
                context.SaveChanges();
            }

            #endregion "Module Creation"

            #region "Role Detail Creation"

            if (!(context.RoleDetails.Any(u => u.RoleDetailId == 1)))
            {
                var roleSAdmin = context.MantiScanRole.First(v => v.RoleId == 1);
                var roleAdmin = context.MantiScanRole.First(v => v.RoleId == 2);
                var roleSuser = context.MantiScanRole.First(v => v.RoleId == 3);
                var roleOther = context.MantiScanRole.First(v => v.RoleId == 4);

                var lstrd = new List<RoleDetail>
                {
                    new RoleDetail
                    {
                        Role = roleSAdmin,
                        Module = context.Modules.First(v => v.ModuleId == 1),
                        Add = true,
                        Edit = true,
                        Delete = true,
                        View = true,
                        IsActive = true
                    },
                    new RoleDetail
                    {
                        Role = roleAdmin,
                        Module = context.Modules.First(v => v.ModuleId == 1),
                        Add = true,
                        Edit = true,
                        Delete = true,
                        View = true,
                        IsActive = true
                    },
                    new RoleDetail
                    {
                        Role = roleSAdmin,
                        Module = context.Modules.First(v => v.ModuleId == 2),
                        Add = true,
                        Edit = true,
                        Delete = true,
                        View = true,
                        IsActive = true
                    },
                    new RoleDetail
                    {
                        Role = roleAdmin,
                        Module = context.Modules.First(v => v.ModuleId == 2),
                        Add = true,
                        Edit = true,
                        Delete = true,
                        View = true,
                        IsActive = true
                    },
                    new RoleDetail
                    {
                        Role = roleSAdmin,
                        Module = context.Modules.First(v => v.ModuleId == 3),
                        Add = true,
                        Edit = true,
                        Delete = true,
                        View = true,
                        IsActive = true
                    },
                    new RoleDetail
                    {
                        Role = roleAdmin,
                        Module = context.Modules.First(v => v.ModuleId == 3),
                        Add = true,
                        Edit = true,
                        Delete = true,
                        View = true,
                        IsActive = true
                    },
                    new RoleDetail
                    {
                        Role = roleSAdmin,
                        Module = context.Modules.First(v => v.ModuleId == 4),
                        Add = true,
                        Edit = true,
                        Delete = true,
                        View = true,
                        IsActive = true
                    },
                    new RoleDetail
                    {
                        Role = roleAdmin,
                        Module = context.Modules.First(v => v.ModuleId == 4),
                        Add = true,
                        Edit = true,
                        Delete = true,
                        View = true,
                        IsActive = true
                    },
                    new RoleDetail
                    {
                        Role = roleSAdmin,
                        Module = context.Modules.First(v => v.ModuleId == 5),
                        Add = true,
                        Edit = true,
                        Delete = true,
                        View = true,
                        IsActive = true
                    },
                    new RoleDetail
                    {
                        Role = roleAdmin,
                        Module = context.Modules.First(v => v.ModuleId == 5),
                        Add = true,
                        Edit = true,
                        Delete = true,
                        View = true,
                        IsActive = true
                    }
                };

                context.RoleDetails.AddRange(lstrd);
                context.SaveChanges();
            }

            #endregion "Role Detail Creation"

            #region "Organization Creation"

            if (!(context.Organizations.Any(u => u.Name == anraConfiguration.DefaultOrganization)))
            {
                var lstOrg = new List<Organization>
                {
                    new Organization
                    {
                        Name = anraConfiguration.DefaultOrganization,
                        Website = "www.anratechnolgies.com",
                        ContactEmail = "admin@flyanra.com"
                    }
                };

                //Main Organization - ANRA Technologies

                context.Organizations.AddRange(lstOrg);
                context.SaveChanges();
            }

            #endregion "Organization Creation"

            var defaultOrganization = context.Organizations.First(v => v.Name == anraConfiguration.DefaultOrganization);

            #region "User Creation"
            if (!(context.User.Any(u => u.UserName == anraConfiguration.DefaultSuperAdmin)))
            {
                #region create superadmin user
                var superAdminUser = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = anraConfiguration.DefaultSuperAdmin,
                    Email = anraConfiguration.DefaultSuperAdmin,
                    FirstName = "John",
                    LastName = "Doe",
                    IsActive = true,                    
                    Organization = defaultOrganization,                    
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                    LastLogin = DateTime.UtcNow
                };

                await signInManager.CreateAsync(superAdminUser, "AnRaBaSe1$");

                await signInManager.SetLockoutEnabledAsync(superAdminUser, false);

                urd = new UserRoleDetail
                {
                    Role = context.MantiScanRole.First(v => v.RoleId == (int)Roles.SuperAdmin),
                    User = superAdminUser,
                    IsActive = true
                };
                context.UserRoleDetails.Add(urd);
                context.SaveChanges();

                #endregion create superadmin user
            }
            #endregion "User Creation"
        }
    }
}
