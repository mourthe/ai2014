using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.AStar
{
    public class SearchResult
    {
        public SearchResult(int cost, List<Point> bestPath)
        {
            BestPath = bestPath;
            Cost = cost;
        }

        public int Cost { get; private set; }

        public List<Point> BestPath { get; private set; }
    }
}
