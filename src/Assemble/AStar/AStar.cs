using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sorting;
using Assemble;

namespace Controller
{
    public class AStar
    {
        private int[] dist;
        private int[] path;
      
        private Map graph;
        private int qtdNodes;
        protected struct Elem
        {
            public Elem(int accCost, Point pos, Point? parent = null)
            {
                this.accCost = accCost;
                this.pos = pos;
                this.parent = parent;
            }

            public int accCost ;
            public Point pos;
            public Point? parent;
        }

        public AStar(Map graph)
        {
            /* initialize the array with infinity distance */
            dist = Enumerable.Repeat(int.MaxValue, qtdNodes).ToArray() ;
            path = Enumerable.Repeat(int.MinValue, qtdNodes).ToArray();
            this.graph = graph;
            this.qtdNodes = Map.Height * Map.Width;
        }

        private void SetNewDistance (int v, int u, int w)
        {
            if (dist[u] > dist[v] + w)
                    {
                        /* we pass by v to get to u */
                        dist[u] = dist[v] + w;
                        path[u] = v;
                    }
        }

        public ICollection<Point> Star( Point posIni, Point posFinal, out int totalCost)
        {            
            var heapBorder = new Heap<Elem>();

         //   Console.WriteLine("cheguei no astar");

            List<Elem> explored = new List<Elem>();
            /* Array to verify if a position was explored */
            var hasExpl = new bool[qtdNodes,qtdNodes];
            var inBorder = new bool[qtdNodes,qtdNodes];
            hasExpl.Initialize();
            inBorder.Initialize();
            
            
            Elem father = new Elem(0, posIni);
            heapBorder.HeapAdd( h(posIni,posFinal), father );


            while (heapBorder.HeapSize() > 0 )
            {
                father = heapBorder.HeapExtractMin().Item3 ;
                inBorder[father.pos.J, father.pos.I] = false;
                if( father.pos.Equals(posFinal) )
                    break;

                explored.Insert(0, father);
                hasExpl[father.pos.J, father.pos.I] = true;


                foreach (var child in father.pos.Neighborhood( posFinal) )
	            {
                    int accChild = 0;
                    accChild = father.accCost + 1;

                    if (hasExpl[child.J, child.I] && accChild >= father.accCost)
                        continue;

                    if (inBorder[child.J, child.I] == false || accChild < father.accCost)
                    {
                        heapBorder.HeapAdd(h(child, posFinal) + accChild, new Elem(accChild, child, father.pos));
                        inBorder[child.J, child.I] = true;
                    }
	            }             
            }

           
            var pathReturn = new List<Point>();
            pathReturn.Insert(0, father.pos );
            totalCost = father.accCost;

            if (!father.parent.HasValue)
               return pathReturn;

            var currParent = father.parent.Value ;
           

           
           
            
            for (int i = 0 , j = 1; i < explored.Count; i++)
			{
                if (explored[i].pos.Equals(currParent) )
                {
                    pathReturn.Insert(j,explored[i].pos);
                    j++;
                    currParent = explored[i].parent.HasValue ? explored[i].parent.Value : posIni  ;
                    //Debug.WriteLine("custo "+explored[i].accCost);
                }
			}
            pathReturn.Reverse();
            return pathReturn.Skip(1).ToList();

        }


        public int h(Point posIni, Point posFin)
        {
            int jIni = posIni.J ;
            int iIni = posIni.I ;
            int jFin = posFin.J ;
            int iFin = posFin.I ;

            return (int)Math.Sqrt( Math.Pow( (jIni - jFin), 2) + Math.Pow( (iIni - iFin), 2))  ;
        }

        
        private Terrain GetTerrainFromPos(Point pos)
        {
          return graph.Points[pos.I, pos.J].Terrain;
        }
    
    }
}
