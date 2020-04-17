using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MantiScanServices.Common
{
    public enum Roles
    {
        [Description("SuperAdmin")]
        SuperAdmin = 1,

        [Description("Admininstrator")]
        Admin = 2,

        [Description("SystemUser")]
        SystemUser = 3,

        [Description("Other")]
        Other = 4

    }

    public enum MediaType : short
    {
        Video = 1,
        Picture = 2
    }

    internal class EnumUtils
    {
        public static string GetDescription(Enum value)
        {
            return
                value
                    .GetType()
                    .GetMember(value.ToString())
                    .FirstOrDefault()
                    ?.GetCustomAttribute<DescriptionAttribute>()
                    ?.Description;
        }
    }
}
