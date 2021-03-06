﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble
{
    public enum Terrain
    {
        Asphalt = 172,
        Earth = 101,
        Grass = 355,
        Stones = 491,
        Building = 495
    }

    public static class TerrainExtensions
    {
        public static int GetCost(this Terrain terrain){
            switch (terrain){
                case Terrain.Asphalt:
                    return -1;
                case Terrain.Earth:
                    return -1;
                case Terrain.Grass:
                    return -1;
                case Terrain.Stones:
                    return -1;
                case Terrain.Building:
                    return 1000000000;
                default:
                    throw new ArgumentOutOfRangeException("Terreno");
            }
        }
    }
}
