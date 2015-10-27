using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using Sample.Application;

namespace Bdf.Sample.Web.API.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SampleAuthenticationAttribute : AuthorizationFilterAttribute
    {
        private readonly IUserService _userServiceImp;
        public SampleAuthenticationAttribute(IUserService userService)
        {
            _userServiceImp = userService;
        }

        public override Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var header = actionContext.Request.Headers.Authorization;
            if (header == null || header.Scheme != SampleIdentity.AuthenticationTypeScheme)
                return base.OnAuthorizationAsync(actionContext, cancellationToken);
            var authParameter = header.Parameter;
            if(string.IsNullOrWhiteSpace(authParameter))
                return null;
            authParameter = Encoding.Default.GetString(Convert.FromBase64String(authParameter));
            var authToken = authParameter.Split(':');
            if (authToken.Length < 2)
                return null;
            if (!_userServiceImp.ValidateUser(authToken[0], authToken[1]))
            {
                return ChallengeAsync(actionContext, cancellationToken);
            }

            var userIdentity =new SampleIdentity(authToken[0], authToken[1]);
            var principal = new GenericPrincipal(userIdentity, null);

            Thread.CurrentPrincipal = principal;

            return base.OnAuthorizationAsync(actionContext, cancellationToken);
        }

        public Task ChallengeAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var parameter = string.Format("realm=\"{0}\"", actionContext.Request.RequestUri.DnsSafeHost);
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            actionContext.Response.Headers.Add("WWW-Authenticate", string.Format("realm=\"{0}\"", parameter));

            return Task.FromResult<object>(null);
        }
    }
}