using System;

namespace Bdf.Sample.Web.Mvc.App_Code
{
    [Flags]
    public enum PermissionKeys
    {
        None = 0,
        Customers = 1,
        SalesReps = 2,
        Buyers = 4,
        Administrators = 8
    }
}