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
            _dist = Enumerable.Repeat(int.MaxValue, _qtdNodes).ToArray();
            _path = Enumerable.Repeat(int.MinValue, _qtdNodes).ToArray();
            _graph = graph;
            _qtdNodes = Map.Height * Map.Width;
        }

        public ICollection<Point> Star(Point initialPostion, Point finalPosition, out int totalCost)
        {
            var heapBorder = new Heap<Element>();
            var explored = new List<Element>();

            /* Array to verify if a position was explored */
            var hasExplored = new bool[_qtdNodes, _qtdNodes];
            var inBorder = new bool[_qtdNodes, _qtdNodes];
            hasExplored.Initialize();
            inBorder.Initialize();

            var father = new Element(0, initialPostion);
            heapBorder.HeapAdd(H(initialPostion, finalPosition), father);
            
            while (heapBorder.HeapSize() > 0)
            {
                father = heapBorder.HeapExtractMin().Item3;
                inBorder[father.Pos.J, father.Pos.I] = false;
                
                // sai do while, chegou no final
                if (father.Pos.Equals(finalPosition))
                    break;

                // pai é marcado como visitado
                explored.Insert(0, father);
                hasExplored[father.Pos.J, father.Pos.I] = true;
                
                foreach (var child in _graph.GetNeighbors(finalPosition))
                {
                    var accChild = 0;
                    accChild = father.AccCost + 1;

                    if (hasExplored[child.J, child.I] && accChild >= father.AccCost)
                        continue;

                    if (inBorder[child.J, child.I] == false || accChild < father.AccCost)
                    {
                        heapBorder.HeapAdd(H(child, finalPosition) + accChild, 
                                                new Element(accChild, child, father.Pos));
                        inBorder[child.J, child.I] = true;
                    }
                }
            }

            var pathReturn = new List<Point>();
            pathReturn.Insert(0, father.Pos);
            totalCost = father.AccCost;

            if (father.Parent == null)
                return pathReturn;

            var currentParent = father.Parent;
            
            // transcreve o melhor caminho para ser retornado
            for (int i = 0, j = 1; i < explored.Count; i++)
            {
                if (explored[i].Pos.Equals(currentParent))
                {
                    pathReturn.Insert(j, explored[i].Pos);
                    j++;
                    currentParent = explored[i].Parent ?? initialPostion;
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

            // distancia entre dois pontos
            return (int)Math.Sqrt(Math.Pow((jIni - jFin), 2) + Math.Pow((iIni - iFin), 2));
        }
    }
}
