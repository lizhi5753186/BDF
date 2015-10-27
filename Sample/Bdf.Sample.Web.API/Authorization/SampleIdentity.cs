using System.Security.Principal;
using Bdf.Sample.Domain.Model;
using Sample.Application;

namespace Bdf.Sample.Web.API.Authorization
{
    public class SampleIdentity : GenericIdentity
    {
        internal const string AuthenticationTypeScheme = "SampleAuth";

        public string Password { get; set; }

        public SampleIdentity(string name, string password)
            : base(name, AuthenticationTypeScheme)
        {
            Password = password;
        }
    }
}