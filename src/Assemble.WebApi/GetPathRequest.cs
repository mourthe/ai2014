﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Assemble.WebApi
{
    [DataContract]
    public class GetPathRequest
    {
        [DataMember(Name = "terrain")]
        public List<int> Terrain { get; set; }

        [DataMember(Name = "elements")]
        public List<int> Elements { get; set; }
    }
}