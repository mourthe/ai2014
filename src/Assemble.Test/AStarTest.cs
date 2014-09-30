﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Assemble.Test
{
    [TestFixture]
    public class AStarTest
    {
        [Test]
        public void AStar_should_return_the_best_path()
        {
            // arrange
            var map = CreateMap();
            var aStar = new AStar.AStar(map);
            
            // act
            var result = aStar.Star(map.Points[0,0], map.Points[0,3]);

            // assert

        }

        private static Map CreateMap()
        {
            var terrain = new List<int>
            {
                171, 495, 495, 101,
                354, 171, 495, 101,
                171, 354, 491, 101,
                491, 171, 171, 491
            };
            
            return new Map(terrain, CreateCharacters(), 4);
        }

        private static List<Character> CreateCharacters()
        {
            var characters = new List<Character>();
            for (var i = 0; i < 6; i++)
            {
                characters.Add(new Character());
            }

            return characters;
        }
    }
}
