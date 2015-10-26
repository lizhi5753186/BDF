using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Bdf.Sample.Web.API.ExceptionHandle;

namespace Bdf.Sample.Web.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            // this is another opportunity to handle exceptions globally,for information:
            // http://stackoverflow.com/questions/15167927/how-do-i-log-all-exceptions-globally-for-a-c-sharp-mvc4-webapi-app
            config.Services.Add(typeof(IExceptionLogger), new ElmahExceptionLogger());
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
