using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Assemble.WebApi
{
    [DataContract]
    public class GetPathResponse
    {
        [DataMember(Name = "steps")]
        public IList<string> Steps { get; set; }

        [DataMember(Name = "party")]
        public IList<string> Party { get; set; }

        [DataMember(Name = "cost")]
        public double Cost { get; set; }
    }
}
