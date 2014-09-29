using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Assemble
{
    [DataContract]
    public class Point
    {
        [DataMember (Name="i")]
        public int I { get; set; }

        [DataMember(Name = "j")]
        public int J { get; set; }

        public Terrain Terrain { get; set; }

        public Point(int i, int j, Terrain terrain)
        {
            this.Terrain = terrain;
            this.I = i;
            this.J = j;
        }
    }
}
