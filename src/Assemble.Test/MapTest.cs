using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.Test
{
    [TestFixture]
    public class MapTest
    {
        [Test]
        public void Get_neighboors_should_throw_return_four_points_picking_one_not_in_the_border()
        {
            // arrange
            var terrain = new List<int> {171, 101, 171, 101, 171, 101, 171, 101, 171};
            var map = new Map(terrain, CreateCharacters(), 3);
            var point = new Point(1, 1, Terrain.Asphalt);

            // act
            var result = map.GetNeighbors(point);

            // assert
            Assert.That(result.Count,Is.EqualTo(4));
            for (var i = 0; i < 4; i++)
            {
                Assert.That(result.ElementAt(i).Terrain, Is.EqualTo(Terrain.Earth));
            }
        }

        [Test]
        public void Get_neighboors_should_throw_return_three_points_picking_one_in_one_border()
        {
            // arrange
            var terrain = new List<int> { 171, 101, 171, 101, 171, 101, 171, 101, 171 };
            var map = new Map(terrain, CreateCharacters(), 3);
            var point = new Point(0, 1, Terrain.Earth);

            // act
            var result = map.GetNeighbors(point);

            // assert
            Assert.That(result.Count, Is.EqualTo(3));
            for (var i = 0; i < 3; i++)
            {
                Assert.That(result.ElementAt(i).Terrain, Is.EqualTo(Terrain.Asphalt));
            }
        }

        [Test]
        public void Get_neighboors_should_throw_return_two_points_picking_one_not_in_the_border()
        {
            // arrange
            var terrain = new List<int> { 171, 101, 171, 101, 171, 101, 171, 101, 171 };
            var map = new Map(terrain, CreateCharacters(), 3);
            var point = new Point(0, 0, Terrain.Asphalt);

            // act
            var result = map.GetNeighbors(point);

            // assert
            Assert.That(result.Count, Is.EqualTo(2));
            for (var i = 0; i < 2; i++)
            {
                Assert.That(result.ElementAt(i).Terrain, Is.EqualTo(Terrain.Earth));
            }
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
