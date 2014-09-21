using System;
using System.Collections.Generic;
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

        public Map(IList<int> terrain, IList<Character> characters)
        { 
            this.Points = new Point[Width, Height];
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
                    if (numberOfavengers < 3 && random.Next(1, 2) % 2 == 0)
                    {
                        character.isConvincible = true;
                        numberOfavengers++;
                    }
                }
            }

            return characters;
        }

        private void BuildTerrain (IList<int> terrain){
            for (int x = 0, i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; x++, j++) {
                    this.Points[i, j] = new Point(i, j, (Terrain)terrain.ElementAt(x));
                } 
            }
        }

        public IList<string> GetBestPath()
        {
            throw new NotImplementedException();
        }

        public IList<Point> GetNeighbors(Point point)
        {
            //TODO: modificar esse metodo
            var neighbors = new List<Point>();

            switch (WichBorder(point))
            {
                case 0: neighbors.Add(this.Points[point.I - 1, point.J]);
                        neighbors.Add(this.Points[point.I + 1, point.J]);
                        neighbors.Add(this.Points[point.I, point.J + 1]);
                        neighbors.Add(this.Points[point.I, point.J - 1]);
                        break;
                case 1: neighbors.Add(this.Points[point.I + 1, point.J]);
                        neighbors.Add(this.Points[point.I, point.J + 1]);
                        break;
                case 2: neighbors.Add(this.Points[point.I + 1, point.J]);
                        neighbors.Add(this.Points[point.I, point.J + 1]);
                        neighbors.Add(this.Points[point.I, point.J - 1]);
                        break;
                case 3: neighbors.Add(this.Points[point.I - 1, point.J]);
                        neighbors.Add(this.Points[point.I + 1, point.J]);
                        neighbors.Add(this.Points[point.I, point.J + 1]);
                        neighbors.Add(this.Points[point.I, point.J - 1]);
                        break;
                case 4: neighbors.Add(this.Points[point.I - 1, point.J]);
                        neighbors.Add(this.Points[point.I + 1, point.J]);
                        neighbors.Add(this.Points[point.I, point.J + 1]);
                        neighbors.Add(this.Points[point.I, point.J - 1]);
                        break;
                case 5: neighbors.Add(this.Points[point.I - 1, point.J]);
                        neighbors.Add(this.Points[point.I + 1, point.J]);
                        neighbors.Add(this.Points[point.I, point.J + 1]);
                        neighbors.Add(this.Points[point.I, point.J - 1]);
                        break;
                case 6: neighbors.Add(this.Points[point.I - 1, point.J]);
                        neighbors.Add(this.Points[point.I + 1, point.J]);
                        neighbors.Add(this.Points[point.I, point.J + 1]);
                        neighbors.Add(this.Points[point.I, point.J - 1]);
                        break;
                case 7: neighbors.Add(this.Points[point.I - 1, point.J]);
                        neighbors.Add(this.Points[point.I + 1, point.J]);
                        neighbors.Add(this.Points[point.I, point.J + 1]);
                        neighbors.Add(this.Points[point.I, point.J - 1]);
                        break;
                case 8: neighbors.Add(this.Points[point.I - 1, point.J]);
                        neighbors.Add(this.Points[point.I + 1, point.J]);
                        neighbors.Add(this.Points[point.I, point.J + 1]);
                        neighbors.Add(this.Points[point.I, point.J - 1]);
                        break;
            } 


            

            return neighbors;
        }

        private int WichBorder(Point point)
        {
            // primeira linha
            if (point.I == 0)
            {
                if (point.J == 0)
                    return 1;
                if (point.J == 41)
                    return 3;
                
                return 2;
            }

            // ultima coluna
            if (point.J == 41)
            {
                if (point.I == 41)
                    return 5;

                return 4;
            }

            // ultima linha       
            if (point.I == 41)
            {
                if (point.J == 0)
                    return 7;
                
                return 6;
            }

            // primera coluna
            if (point.J == 0)
            {
                if (point.I == 41)
                    return 7;

                return 8;
            }

            return 0;
        }
    }
}
