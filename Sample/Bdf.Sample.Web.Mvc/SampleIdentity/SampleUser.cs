using System;
using Bdf.Sample.Domain.Model;
using Microsoft.AspNet.Identity;

namespace Bdf.Sample.Web.Mvc.SampleIdentity
{
    public class SampleUser : User, IUser<Guid>
    {
    }
}