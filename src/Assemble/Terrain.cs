using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble
{
    public enum Terrain
    {
        Asphalt = 171,
        Earth = 101,
        Grass = 354,
        Stones = 491,
        Building = 495
    }

    public static class TerrainExtensions
    {
        public static uint GetCost(this Terrain terrain){
            switch (terrain){
                case Terrain.Asphalt:
                    return 1;
                case Terrain.Earth:
                    return 3;
                case Terrain.Grass:
                    return 5;
                case Terrain.Stones:
                    return 10;
                case Terrain.Building:
                    return uint.MaxValue;
                default:
                    throw new ArgumentOutOfRangeException("Terreno");
            }
        }
    }
}
