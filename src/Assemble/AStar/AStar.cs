using System;
using System.Collections.Generic;
using System.Linq;
using Sorting;

namespace Assemble.AStar
{
    public class AStar
    {
        private readonly int[] _dist;
        private readonly int[] _path;

        private readonly Map _graph;
        private readonly int _qtdNodes;

        protected struct Element
        {
            public Element(int accCost, Point pos, Point parent = null)
            {
                AccCost = accCost;
                Pos = pos;
                Parent = parent;
            }

            public int AccCost;
            public Point Pos;
            public Point Parent;
        }

        public AStar(Map graph)
        {
            /* initialize the array with infinity distance */
            _dist = Enumerable.Repeat(int.MaxValue, _qtdNodes).ToArray();
            _path = Enumerable.Repeat(int.MinValue, _qtdNodes).ToArray();
            _graph = graph;
            _qtdNodes = Map.Height * Map.Width;
        }

        private void SetNewDistance(int v, int u, int w)
        {
            if (_dist[u] <= _dist[v] + w) return;
            
            /* we pass by v to get to u */
            _dist[u] = _dist[v] + w;
            _path[u] = v;
        }

        public ICollection<Point> Star(Point posIni, Point posFinal, out int totalCost)
        {
            var heapBorder = new Heap<Element>();
            var explored = new List<Element>();

            /* Array to verify if a position was explored */
            var hasExpl = new bool[_qtdNodes, _qtdNodes];
            var inBorder = new bool[_qtdNodes, _qtdNodes];
            hasExpl.Initialize();
            inBorder.Initialize();

            var father = new Element(0, posIni);
            heapBorder.HeapAdd(H(posIni, posFinal), father);
            
            while (heapBorder.HeapSize() > 0)
            {
                father = heapBorder.HeapExtractMin().Item3;
                inBorder[father.Pos.J, father.Pos.I] = false;
                
                if (father.Pos.Equals(posFinal))
                    break;

                explored.Insert(0, father);
                hasExpl[father.Pos.J, father.Pos.I] = true;

                foreach (var child in _graph.GetNeighbors(posFinal))
                {
                    var accChild = 0;
                    accChild = father.AccCost + 1;

                    if (hasExpl[child.J, child.I] && accChild >= father.AccCost)
                        continue;

                    if (inBorder[child.J, child.I] == false || accChild < father.AccCost)
                    {
                        heapBorder.HeapAdd(H(child, posFinal) + accChild, new Element(accChild, child, father.Pos));
                        inBorder[child.J, child.I] = true;
                    }
                }
            }

            var pathReturn = new List<Point>();
            pathReturn.Insert(0, father.Pos);
            totalCost = father.AccCost;

            if (!father.Parent.HasValue)
                return pathReturn;

            var currParent = father.Parent.Value;
            
            for (int i = 0, j = 1; i < explored.Count; i++)
            {
                if (explored[i].Pos.Equals(currParent))
                {
                    pathReturn.Insert(j, explored[i].Pos);
                    j++;
                    currParent = explored[i].Parent.HasValue ? explored[i].Parent.Value : posIni;
                }
            }
            pathReturn.Reverse();
            return pathReturn.Skip(1).ToList();

        }

        public int H(Point posIni, Point posFin)
        {
            var jIni = posIni.J;
            var iIni = posIni.I;
            var jFin = posFin.J;
            var iFin = posFin.I;

            return (int)Math.Sqrt(Math.Pow((jIni - jFin), 2) + Math.Pow((iIni - iFin), 2));
        }

        private Terrain GetTerrainFromPos(Point pos)
        {
            return _graph.Points[pos.I, pos.J].Terrain;
        }
    }
}
