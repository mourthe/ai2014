using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.AStar
{
    class AStarNode
    {
        public double Cost { get; private set; }

        public AStarNode(double cost)
        {
            this.Cost = cost;
        }
    }
}
