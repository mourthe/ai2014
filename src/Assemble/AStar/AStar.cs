using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.AStar
{
    public class AStar
    {
        private Map _map;
        private Point _startPoint;
        private Point _endPoint;

        private double _cost = 0;

        public AStar(Map map, Point startPoint, Point endPoint)
        {
            _map = map;
            _startPoint = startPoint;
            _endPoint = endPoint;
        }


        public SearchResult Search()
        {
            var bestPath = new List<Point>();



            return new SearchResult(_cost, bestPath);
        }
    }
}
