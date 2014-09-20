using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assemble
{
    public class Point
    {
        public int I { get; set; }
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
