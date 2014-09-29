using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble
{
    public class Map
    {
        public Point[,] Points { get; set; }
        public IList<Character> Characters { get; set; }
        
        public static readonly int Height = 42;
        public static readonly int Width = 42;
        private readonly int _size;

        public Map(IList<int> terrain, IList<Character> characters, int size = 42) 
        {
            _size = size;
            this.Points = new Point[_size, _size];
            this.BuildTerrain(terrain);
            this.Characters = this.CreateCharacters(characters);
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
            var filteredNames = this.getThreeConvincedNames(TravellingSalesman.Algorithm.Execute(this));
            return this.getPathInDirections(filteredNames);
        }

        private IList<string> getPathInDirections(List<string> names)
        {
            throw new NotImplementedException();
        }

        private List<string> getThreeConvincedNames(List<string> names)
        {
            var count = 0;
            List<string> filteredNames = new List<string>();
            foreach (var name in names)
            {
                filteredNames.Add(name);
                if (this.Characters.FirstOrDefault(c => c.Name == name).isConvincible == true)
                {
                    count++;
                    if (count == 3)
                    {
                        break;
                    }
                }
            }

            return filteredNames;
        }

        public IList<Point> GetNeighbors(Point point)
        {
            var neighbors = new List<Point>();

            // não está na primeira coluna
            if (point.J != 0)
                neighbors.Add(Points[point.I, point.J - 1]);
            
            // não está na ultima coluna
            if (point.J != 41)
                neighbors.Add(Points[point.I, point.J + 1]);
            
            // não está na primeira linha
            if (point.I != 0)
                neighbors.Add(Points[point.I - 1, point.J]);
            
            // não está na ultima linha
            if (point.I != 41)
                neighbors.Add(Points[point.I + 1, point.J]);
            
            return neighbors;
        }
    }
}
