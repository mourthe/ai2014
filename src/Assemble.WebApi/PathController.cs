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
        public IList<string> GetPath(IList<int> terrain, IList<Character> characters)
        {
            return new AssembleService().GetBestPath(terrain, characters); 
        }
    }
}