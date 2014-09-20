using System.Collections.Generic;

namespace Assemble.AStar
{
    public class SearchResult
    {
        public double Cost { get; private set; }

        public IList<Point> BestPath { get; private set; } 

        public SearchResult(double cost, IList<Point> bestPath)
        {
            BestPath = bestPath;
            Cost = cost;
        }
    }
}
