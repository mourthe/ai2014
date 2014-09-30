using System;
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
            var terrain = new List<int>
            {
                171, 495, 495, 101,
                354, 171, 495, 101,
                171, 354, 491, 101,
                491, 171, 171, 491
            };

            var map = new Map(terrain, CreateCharacters(), 4);
            var aStar = new AStar.AStar(map);
            
            // act
            var result = aStar.Star(map.Points[0,0], map.Points[0,3]);

            // assert
            Assert.That(result.Cost, Is.EqualTo(31));
        }

        [Test]
        public void AStar_should_return_the_best_path_inverted()
        {
            // arrange
            var terrain = new List<int>
            {
                171, 495, 495, 101,
                354, 171, 495, 101,
                171, 354, 491, 101,
                491, 171, 171, 491
            };

            var map = new Map(terrain, CreateCharacters(), 4);
            var aStar = new AStar.AStar(map);

            // act
            var result = aStar.Star(map.Points[0, 3], map.Points[0, 0]);

            // assert
            Assert.That(result.Cost, Is.EqualTo(31));
        }

        [Test]
        public void AStar_should_return_the_best_path_just_to_be_sure()
        {
            // arrange
            var terrain = new List<int>
            {
                101, 101, 495, 171,
                101, 495, 495, 171,
                354, 171, 495, 491,
                354, 171, 171, 491
            };

            var map = new Map(terrain, CreateCharacters(), 4);
            var aStar = new AStar.AStar(map);

            // act
            var result = aStar.Star(map.Points[0, 1], map.Points[0, 3]);

            // assert
            Assert.That(result.Cost, Is.EqualTo(39));
        }

        [Test]
        public void AStar_should_return_the_best_path_five_by_five()
        {
            // arrange
            var terrain = new List<int>
            {
                101, 101, 495, 171, 171,
                101, 495, 495, 171, 101,
                354, 171, 495, 491, 101,
                354, 171, 171, 491, 171,
                495, 495, 354, 171, 171
            };

            var map = new Map(terrain, CreateCharacters(), 5);
            var aStar = new AStar.AStar(map);

            // act
            var result = aStar.Star(map.Points[0, 1], map.Points[0, 3]);

            // assert
            Assert.That(result.Cost, Is.EqualTo(33));
        }

        [Test]
        public void AStar_should_return_the_best_path_ten_by_ten()
        {
            // arrange
            var terrain = new List<int>
            {
                101, 101, 495, 171, 171, 101, 101, 495, 171, 171,
                101, 495, 495, 171, 101, 101, 495, 495, 171, 101,
                354, 171, 495, 491, 101, 354, 171, 495, 491, 101,
                354, 171, 171, 491, 171, 495, 171, 171, 491, 171,
                495, 495, 354, 171, 171, 495, 354, 354, 171, 171,
                101, 101, 495, 171, 171, 101, 101, 495, 171, 171,
                101, 495, 495, 171, 101, 101, 495, 495, 171, 101,
                354, 171, 495, 491, 101, 354, 171, 495, 491, 101,
                354, 171, 171, 491, 171, 354, 171, 171, 491, 171,
                495, 495, 354, 171, 171, 495, 495, 354, 171, 171
            };

            var map = new Map(terrain, CreateCharacters(), 10);
            var aStar = new AStar.AStar(map);

            // act
            var result = aStar.Star(map.Points[6, 4], map.Points[0, 1]);

            // assert
            Assert.That(result.Cost, Is.EqualTo(28));
        }

                [Test]
        public void AStar_should_return_the_best_path_ten_by_ten_just_to_be_sure()
        {
            // arrange
            var terrain = new List<int>
            {
                101, 101, 495, 171, 171, 101, 101, 495, 171, 171,
                101, 495, 495, 171, 101, 101, 495, 495, 171, 101,
                354, 171, 495, 491, 101, 354, 171, 495, 491, 101,
                354, 171, 171, 491, 171, 495, 171, 171, 491, 171,
                495, 495, 354, 171, 171, 495, 354, 354, 171, 171,
                101, 101, 495, 171, 171, 101, 101, 495, 171, 171,
                101, 495, 495, 171, 101, 101, 495, 495, 171, 101,
                354, 171, 495, 491, 101, 354, 171, 495, 491, 101,
                354, 171, 171, 491, 171, 354, 171, 171, 491, 171,
                495, 495, 354, 171, 171, 495, 495, 354, 171, 171
            };

            var map = new Map(terrain, CreateCharacters(), 10);
            var aStar = new AStar.AStar(map);

            // act
            var result = aStar.Star(map.Points[8, 1], map.Points[3, 9]);

            // assert
            Assert.That(result.Cost, Is.EqualTo(34));
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
