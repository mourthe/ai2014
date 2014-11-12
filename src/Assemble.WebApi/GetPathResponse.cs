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
        [DataMember(Name = "actions")]
        public IList<Action> Actions { get; set; }

        [DataMember(Name = "visited")]
        public IList<string> Visited { get; set; }

        [DataMember(Name = "cost")]
        public List<int> Cost { get; set; }

        [DataMember(Name = "elemsToDestroy")]
        public List<Point> ElemsToDestroy { get; set; }

        [DataMember(Name = "kindToDestroy")]
        public string KindToDestroy { get; set; }
    }
}
