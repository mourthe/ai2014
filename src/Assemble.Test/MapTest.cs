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
        #region GetNeightboors

        [Test]
        public void Get_neighboors_should_return_four_points_picking_one_not_in_the_border()
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
        public void Get_neighboors_should_return_three_points_picking_one_in_one_border()
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

        [Test]
        public void Get_neighboors_should_return_one_points_picking_one_in_the_border_close_to_a_building()
        {
            // arrange
            var terrain = new List<int> { 171, 495, 171, 101, 171, 101, 171, 101, 171 };
            var map = new Map(terrain, CreateCharacters(), 3);
            var point = new Point(0, 0, Terrain.Asphalt);

            // act
            var result = map.GetNeighbors(point);

            // assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ElementAt(0).Terrain, Is.EqualTo(Terrain.Earth));
        }

        [Test]
        public void Get_neighboors_should_return_two_points_picking_one_in_one_border_close_to_a_building()
        {
            // arrange
            var terrain = new List<int> { 495, 101, 171, 101, 171, 101, 171, 101, 171 };
            var map = new Map(terrain, CreateCharacters(), 3);
            var point = new Point(0, 1, Terrain.Earth);

            // act
            var result = map.GetNeighbors(point);

            // assert
            Assert.That(result.Count, Is.EqualTo(2));
            for (var i = 0; i < 2; i++)
            {
                Assert.That(result.ElementAt(i).Terrain, Is.EqualTo(Terrain.Asphalt));
            }
        }

        [Test]
        public void Get_neighboors_should_return_three_points_picking_one_not_in_the_border_close_to_one_building()
        {
            // arrange
            var terrain = new List<int> { 171, 495, 171, 101, 171, 101, 171, 101, 171 };
            var map = new Map(terrain, CreateCharacters(), 3);
            var point = new Point(1, 1, Terrain.Asphalt);

            // act
            var result = map.GetNeighbors(point);

            // assert
            Assert.That(result.Count, Is.EqualTo(3));
            for (var i = 0; i < 3; i++)
            {
                Assert.That(result.ElementAt(i).Terrain, Is.EqualTo(Terrain.Earth));
            }
        }

        [Test]
        public void Get_neighboors_should_return_no_points_picking_one_surrounded_by_buildings()
        {
            // arrange
            var terrain = new List<int> {171, 495, 171, 495, 171, 495, 171, 495, 171};
            var map = new Map(terrain, CreateCharacters(), 3);
            var point = new Point(1, 1, Terrain.Asphalt);

            // act
            var result = map.GetNeighbors(point);

            // assert
            Assert.That(result, Is.Empty);
        }

        #endregion

        #region Result

        [Test]
        public void Result_Matrix_should_be_build_Correctly()
        {
            // arrange5
            var map = CreateMap(CreateFullCharacters()); 

            // act
            map.InitializeResult();

            // assert
            Assert.That(map.Result[0, 1].Cost, Is.EqualTo(28));
            Assert.That(map.Result[4, 3].Cost, Is.EqualTo(34));
            Assert.That(map.Result[4, 5].Cost, Is.EqualTo(9));
            Assert.That(map.Result[0, 3].Cost, Is.EqualTo(23));
            Assert.That(map.Result[1, 4].Cost, Is.EqualTo(41));
        }

        [Test]
        public void Result_Matrix_should_be_build_Correctly_With_the_path()
        {
            // arrange5
            var map = CreateMap(CreateFullCharacters());

            // act
            map.InitializeResult();

            // assert
            Assert.That(map.Result[4, 5].Cost, Is.EqualTo(9));
            Assert.That(map.Result[4, 5].BestPath.Count, Is.EqualTo(4));
            Assert.That(map.Result[4, 5].BestPath.ElementAt(0).Equals(new Point(8, 2, Terrain.Asphalt)), Is.True);
            Assert.That(map.Result[4, 5].BestPath.ElementAt(1).Equals(new Point(9, 2, Terrain.Grass)), Is.True);
            Assert.That(map.Result[4, 5].BestPath.ElementAt(2).Equals(new Point(9, 3, Terrain.Asphalt)), Is.True);
            Assert.That(map.Result[4, 5].BestPath.ElementAt(3).Equals(new Point(9, 4, Terrain.Asphalt)), Is.True);
        }

        #endregion

        private static List<Character> CreateCharacters()
        {
            var characters = new List<Character>();
            for (var i = 0; i < 6; i++)
            {
                characters.Add(new Character());
            }

            return characters;
        }

        private static List<Character> CreateFullCharacters()
        {
            var characters = new List<Character>
            {
                new Character() {Index = 0, Name = "Nick", Position = new Point(6, 4, Terrain.Earth)},
                new Character() {Index = 1, Name = "Tony", Position = new Point(0, 1, Terrain.Earth)},
                new Character() {Index = 2, Name = "Steve", Position = new Point(2, 5, Terrain.Grass)},
                new Character() {Index = 3, Name = "Hulk", Position = new Point(3, 9, Terrain.Asphalt)},
                new Character() {Index = 4, Name = "Thor", Position = new Point(8, 1, Terrain.Asphalt)},
                new Character() {Index = 5, Name = "Hawkeye", Position = new Point(9, 4, Terrain.Asphalt)},
                new Character() {Index = 6, Name = "Black Widow", Position = new Point(7, 8, Terrain.Stones)}
            };

            return characters;
        }

        private static Map CreateMap(IList<Character> characters)
        {
            var terrain = new List<int>()
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

            return new Map(terrain, characters, 10);
        }
    }
}
