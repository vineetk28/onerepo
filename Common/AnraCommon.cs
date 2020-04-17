using MantiScanServices.DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantiScanServices.Common
{
    public class AnraCommon
    {
        public static bool IsAdmin(string emailId, MantiDbContext dbContext)
        {
            var roles = from u in dbContext.Users
                        join ur in dbContext.UserRoleDetails on u.Id equals ur.UserId
                        join r in dbContext.MantiScanRole on ur.RoleId equals r.RoleId
                        where u.Email == emailId
                        select r;

            return (roles.ToList().Any(a => a.RoleId == Convert.ToInt32(Roles.SuperAdmin)));
        }

        public static bool IsAdministrator(string emailId, MantiDbContext dbContext)
        {
            var roles = from u in dbContext.Users
                        join ur in dbContext.UserRoleDetails on u.Id equals ur.UserId
                        join r in dbContext.MantiScanRole on ur.RoleId equals r.RoleId
                        where u.Email == emailId
                        select r;

            return (roles.ToList().Any(a => a.RoleId == Convert.ToInt32(Roles.Admin)));
        }

        public static string GetRole(string emailId, MantiDbContext dbContext)
        {
            var roleName = "others";

            var roles = from u in dbContext.Users
                        join ur in dbContext.UserRoleDetails on u.Id equals ur.UserId
                        join r in dbContext.MantiScanRole on ur.RoleId equals r.RoleId
                        where u.Email == emailId
                        select r;

            if (roles.Count(a => a.RoleId == Convert.ToInt32(Roles.SuperAdmin)) > 0)
                roleName = "superadmin";
            else if (roles.Count(a => a.RoleId == Convert.ToInt32(Roles.Admin)) > 0)
                roleName = "admin";
            else if (roles.Count(a => a.RoleId == Convert.ToInt32(Roles.SystemUser)) > 0)
                roleName = "systemuser";
            else
                roleName = "others";

            return roleName;
        }
    }
}
