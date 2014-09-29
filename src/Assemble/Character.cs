using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Assemble
{
    [DataContract]
    public class Character
    {
        [DataMember(Name = "idx")]
        public int Index { get; set; }

        [DataMember (Name="name")]
        public string Name { get; set; }

        public bool isConvincible { get; set; }

        [DataMember (Name="position")]
        public Point Position { get; set; }
    }
}
