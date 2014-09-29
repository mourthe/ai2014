﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Assemble.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/path",
                defaults: new {
                    controller = "Path",
                    action = "Post"
                }
            );
        }
    }
}
