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
        public List<Character> CharactersWithNick { get; private set; }

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
            
            // copia charactersWithNick e o tira da lista
            this.CharactersWithNick = characters.ToList();
            characters.RemoveAt(0);

            this.Characters = this.CreateCharacters(characters);
//            this.InitializeResult();
            //Mapa tá pronto, vc tem o mapa!
        }

        private IList<Character> CreateCharacters(IList<Character> characters)
        {
            int numberOfavengers = 0; 
            Random random = new Random(Guid.NewGuid().GetHashCode());

            // fill the terrain atribute on characters
            foreach (var character in characters)
            {
                character.Position.Terrain = Points[character.Position.I, character.Position.J].Terrain;
            }

            // fill the terrain atribute on charactersWithNick
            foreach (var character in CharactersWithNick)
            {
                character.Position.Terrain = Points[character.Position.I, character.Position.J].Terrain;
            }

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

            return this.GetPathInDirections(TravellingSalesman.Algorithm.Execute(this));
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
            var currPos = new Point(22, 18, this.Points[22, 18].Terrain);
            var steps = new List<string>();
            names.Add("Nick");
            foreach (var name in names)
            {
                try
                {
                    var dest = GetPointFromName(name);
                    var path = GetPathInPoints(currPos, dest);

                    var stepDests = path as Point[] ?? path.ToArray();
                    foreach (var stepDest in stepDests)
                    {
                        steps.Add(GetStep(currPos, stepDest));
                        currPos = stepDest;
                    }

                    //Adiciona step de stop para poder rolar a animação da conversa de convencimento
                    steps.Add("stop");
                    currPos = stepDests.Last();
                }
                catch (Exception e)
                {                    
                    throw e;
                }
            }

            return steps;
        }

        private static string GetStep(Point currPos, Point dest)
        {
            if (dest.I > currPos.I)
            {
                return "s";
            }

            if (dest.I < currPos.I)
            {
                return "n";
            }

            if (dest.J > currPos.J)
            {
                return "e";
            }

            if (dest.J < currPos.J)
            {
                return "w";
            }

            return "stop";
        }

        private IEnumerable<Point> GetPathInPoints(Point currPos, Point dest)
        {
            int current = 0, destination = 0;


            // checa se esta nos characters
            for (int i = 0, j = 0; i < CharactersWithNick.Count; i++, j++)
            {
                if (currPos.Equals(CharactersWithNick[i].Position))
                {
                    current = i;
                }

                if (dest.Equals(CharactersWithNick[j].Position))
                {
                    destination = j;
                }
            }

            return this.Result[current, destination].BestPath;
        }

        private Point GetPointFromName(string name)
        {
            return CharactersWithNick.FirstOrDefault(c => c.Name == name).Position;
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

            for (var i = 0; i < CharactersWithNick.Count; i++)
            {
                for (var j = i; j < CharactersWithNick.Count; j++)
                {
                    if (j != i)
                    {
                        result[i, j] = aStar.Star(CharactersWithNick[i].Position, CharactersWithNick[j].Position);
                        result[j, i] = aStar.Star(CharactersWithNick[j].Position, CharactersWithNick[i].Position);

                        // matriz é simétrica em relação a diagonal
                        // result[j, i] = new SearchResult(result[i, j].Cost, result[i, j].BestPath.ToList());
                        // result[j, i].BestPath.Reverse();
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
