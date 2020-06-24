using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;
using System.Web.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Serialization;

namespace FRS_WorkerRoles
{
    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();
            
            config.Routes.MapHttpRoute(
                "Default",
                "{controller}/{db}",
                new { db = RouteParameter.Optional  });

            app.UseWebApi(config);
        }
    }
}