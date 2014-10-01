using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Assemble.AStar;

namespace Assemble
{
    public class Map
    {
        public Point[,] Points { get; set; }
        public IList<Character> Characters { get; set; }
        public SearchResult[,] Result { get; set; }

        public static readonly int Height = 42;
        public static readonly int Width = 42;
        private readonly int _size;

        /// <summary>
        /// Construtor da classe Map
        /// </summary>
        /// <param name="characters">Lista com todos os personagens inclusive a casa do nick</param>
        public Map(IList<int> terrain, IList<Character> characters, int size = 42) 
        {
            _size = size;
            this.Result = null;
            this.Points = new Point[_size, _size];
            this.BuildTerrain(terrain);
            this.Characters = this.CreateCharacters(characters);
//            this.InitializeResult();
            //Mapa tá pronto, vc tem o mapa!
        }

        private IList<Character> CreateCharacters(IList<Character> characters)
        {
            int numberOfavengers = 0; 
            Random random = new Random(Guid.NewGuid().GetHashCode());

            while (numberOfavengers < 3)
            {
                foreach (var character in characters)
                {
                    if (numberOfavengers < 3 && random.NextDouble() > 0.5)
                    {
                        character.isConvincible = true;
                        numberOfavengers++;
                    }
                }
            }

            return characters;
        }

        private void BuildTerrain (IList<int> terrain){
            for (int x = 0, i = 0; i < _size; i++)
            {
                for (var j = 0; j < _size; x++, j++)
                {
                    this.Points[i, j] = new Point(i, j, (Terrain)terrain.ElementAt(x));
                } 
            }
        }

        public IList<string> GetBestPath()
        {
            if (this.Result == null)
            {
                this.InitializeResult();
            }

            var filteredNames = this.GetThreeConvincedNames(TravellingSalesman.Algorithm.Execute(this));
            return this.GetPathInDirections(filteredNames);
        }

        public IList<Point> GetNeighbors(Point point)
        {
            var neighbors = new List<Point>();

            // não está na primeira coluna e o vizinho não é predio
            if (point.J != 0 && Points[point.I, point.J - 1].Terrain != Terrain.Building)
                neighbors.Add(Points[point.I, point.J - 1]);

            // não está na ultima coluna e o vizinho não é predio
            if (point.J != _size - 1 && Points[point.I, point.J + 1].Terrain != Terrain.Building)
                neighbors.Add(Points[point.I, point.J + 1]);

            // não está na primeira linha e o vizinho não é predio
            if (point.I != 0 && Points[point.I - 1, point.J].Terrain != Terrain.Building)
                neighbors.Add(Points[point.I - 1, point.J]);

            // não está na ultima linha e o vizinho não é predio
            if (point.I != _size - 1 && Points[point.I + 1, point.J].Terrain != Terrain.Building)
                neighbors.Add(Points[point.I + 1, point.J]);

            return neighbors;
        }

        private IList<string> GetPathInDirections(List<string> names)
        {
            var currPos = new Point(23, 19, this.Points[23, 19].Terrain);
            var currentPoint = new Point(0, 0, this.Points[0, 0].Terrain);
            var steps = new List<string>();
            foreach (var name in names)
            {
                var dest = GetPointFromName(name);
                var path = GetPathInPoints(currPos, dest);
                
                //Para cada ponto na rota entre a posição atual e o destion, adicionar o step que se deve fazer
                foreach (var stepDest in path)
                {
                    steps.Add(GetStep(currPos, stepDest));
                    currentPoint = stepDest;
                }

                //Adiciona step de stop para poder rolar a animação da conversa de convencimento
                steps.Add("stop");
                currPos = currentPoint;
            }

            return steps;
        }

        private static string GetStep(Point currPos, Point dest)
        {
            if (dest.I > currPos.I)
            {
                return "down";
            }

            if (dest.I < currPos.I)
            {
                return "up";
            }

            if (dest.J > currPos.J)
            {
                return "left";
            }

            if (dest.J > currPos.J)
            {
                return "right";
            }

            return "stop";
        }

        private IEnumerable<Point> GetPathInPoints(Point currPos, Point dest)
        {
            int current = 0, destination = 0;
            for (int i = 0, j = 0; i < this.Characters.Count; i++, j++)
            {
                if (currPos.Equals(Characters[i].Position))
                {
                    current = i;
                }

                if (dest.Equals(Characters[j].Position))
                {
                    destination = j;
                }
            }

            return this.Result[current, destination].BestPath;
        }

        private Point GetPointFromName(string name)
        {
            return this.Characters.FirstOrDefault(c => c.Name == name).Position;
        }

        private List<string> GetThreeConvincedNames(IEnumerable<string> names)
        {
            var count = 0;
            var filteredNames = new List<string>();
            
            foreach (var name in names)
            {
                if (this.Characters.FirstOrDefault(c => c.Name == name).isConvincible)
                {
                    filteredNames.Add(name);

                    count++;
                    if (count == 3)
                    {
                        break;
                    }
                }
            }

            return filteredNames;
        }
        
        public void InitializeResult()
        {
            var result = new SearchResult[7, 7];
            var aStar = new AStar.AStar(this);

            for (var i = 0; i < this.Characters.Count; i++)
            {
                for (var j = i; j < this.Characters.Count; j++)
                {
                    if (j != i)
                    {
                        result[i, j] = aStar.Star(Characters[i].Position, Characters[j].Position);
                        
                        // matriz é simétrica em relação a diagonal
                        result[j, i] = result[i, j];
                    }
                    else
                    {
                        result[i, j] = new SearchResult(0, null);
                    }
                }
            }

            this.Result = result;
        }
    }
}
