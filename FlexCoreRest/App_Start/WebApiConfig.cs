using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace FlexCoreRest
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "Pagos",
                routeTemplate: "pagar",
                defaults: new { controller = "pagos" }
            );
        }
    }
}
