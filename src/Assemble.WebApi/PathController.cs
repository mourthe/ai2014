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
        public GetPathResponse Post(GetPathRequest request)
        {
            //return new List<string>() { "left", "left", "up", "up", "up", "right", "right", "right", "right", "right", "up", "up", "up"};
            var response = new GetPathResponse();
            double cost;
            IList<string> party;
            response.Steps = AssembleService.GetBestPath(request.Terrain, request.Characters, out cost, out party);
            response.Cost = cost;
            response.Party = party;

            return response;
        }
    }
}