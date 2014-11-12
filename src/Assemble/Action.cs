using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Assemble
{
    [DataContract]
    public class Action
    {
        [DataMember(Name = "step")]
        public string Step { get; set; }

        [DataMember(Name = "cost")]
        public int Cost { get; set; }

        [DataMember(Name = "elemsToDestroy")]
        public Point ElemToDestroy { get; set; }

        [DataMember(Name = "kindToDestroy")]
        public string KindToDestroy { get; set; }
    }
}
