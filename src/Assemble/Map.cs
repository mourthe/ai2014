﻿using System;
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

        public Map(IList<int> terrain, IList<Character> characters)
        { 
            this.Points = new Point[42, 42];
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
            for (int x = 0, i = 0; i < 42; i++)
            {
                for (int j = 0; j < 42; x++, j++) {
                    this.Points[i, j] = new Point(i, j, (Terrain)terrain.ElementAt(x));
                } 
            }
        }

        public IList<string> GetBestPath()
        {
            throw new NotImplementedException();
        }
    }
}
