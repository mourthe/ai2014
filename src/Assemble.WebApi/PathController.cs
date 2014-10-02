using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assemble.WebApi
{
    public class PathController : ApiController
    {
        // GET api/<controller>
        // GET api/path

        [HttpPost]
        public IList<string> Post(GetPathRequest request)
        {
            //return new List<string>() { "left", "left", "up", "up", "up", "right", "right", "right", "right", "right", "up", "up", "up"};
            return new AssembleService().GetBestPath(request.Terrain, request.Characters); 
        }
    }
}