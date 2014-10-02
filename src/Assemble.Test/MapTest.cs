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
            var terrain = new List<int> {172, 101, 172, 101, 172, 101, 172, 101, 172};
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
            var terrain = new List<int> { 172, 101, 172, 101, 172, 101, 172, 101, 172 };
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
            var terrain = new List<int> { 172, 101, 172, 101, 172, 101, 172, 101, 172 };
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
            var terrain = new List<int> { 172, 495, 172, 101, 172, 101, 172, 101, 172 };
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
            var terrain = new List<int> { 495, 101, 172, 101, 172, 101, 172, 101, 172 };
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
            var terrain = new List<int> { 172, 495, 172, 101, 172, 101, 172, 101, 172 };
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
            var terrain = new List<int> {172, 495, 172, 495, 172, 495, 172, 495, 172};
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

        [Test]
        public void TestRealResult()
        {
            // arrange
            var characters = CreateRealCharacters();
            var map = CreateRealMap(characters);

            // act
            map.InitializeResult();

            // assert
            // assert all costs and paths
            for (var i = 0; i < 7; i++)
            {
                for (var j = i; j < 7; j++)
                {
                    if (i == j) continue;
                    //Assert.That(map.Result[i, j].Cost, Is.EqualTo(map.Result[j, i].Cost));
                    Assert.That(IsNeighbor(map,  map.CharactersWithNick[i].Position, map.Result[i, j].BestPath.First()), Is.True);
                    Assert.That(map.CharactersWithNick[i].Position, Is.EqualTo(map.Result[j, i].BestPath.Last()));
                }
            }
        }

        //[Test]
        public void Result_Genetic_should_be_build_Best_Path()
        {
            // arrange5
            var map = CreateMap(CreateFullCharacters());

            var path = map.GetBestPath();

            // assert
            //Assert.That(map.Result[4, 5].Cost, Is.EqualTo(9));
            //Assert.That(map.Result[4, 5].BestPath.Count, Is.EqualTo(4));
            //Assert.That(map.Result[4, 5].BestPath.ElementAt(0).Equals(new Point(8, 2, Terrain.Asphalt)), Is.True);
            //Assert.That(map.Result[4, 5].BestPath.ElementAt(1).Equals(new Point(9, 2, Terrain.Grass)), Is.True);
            //Assert.That(map.Result[4, 5].BestPath.ElementAt(2).Equals(new Point(9, 3, Terrain.Asphalt)), Is.True);
            //Assert.That(map.Result[4, 5].BestPath.ElementAt(3).Equals(new Point(9, 4, Terrain.Asphalt)), Is.True);
        }

        #endregion

        [Test]
        public void Maps_Create_character_makes_three_avengers_convincible()
        {
            // arrange
            var characters = CreateFullCharacters();

            // act
            var map = CreateMap(characters);

            // assert
            Assert.That( map.Characters.Count(character => character.isConvincible), Is.EqualTo(3));
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
                101, 101, 495, 172, 172, 101, 101, 495, 172, 172,
                101, 495, 495, 172, 101, 101, 495, 495, 172, 101,
                355, 172, 495, 491, 101, 355, 172, 495, 491, 101,
                355, 172, 172, 491, 172, 495, 172, 172, 491, 172,
                495, 495, 355, 172, 172, 495, 355, 355, 172, 172,
                101, 101, 495, 172, 172, 101, 101, 495, 172, 172,
                101, 495, 495, 172, 101, 101, 495, 495, 172, 101,
                355, 172, 495, 491, 101, 355, 172, 495, 491, 101,
                355, 172, 172, 491, 172, 355, 172, 172, 491, 172,
                495, 495, 355, 172, 172, 495, 495, 355, 172, 172
            };

            return new Map(terrain, characters, 10);
        }

        private static Map CreateRealMap(IList<Character> characters)
        {
            var terrain = new List<int>()
            {
                355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 355, 355, 355, 355, 491, 491, 491, 491, 491, 172, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 491, 495, 495, 495, 495, 495, 491, 495, 495, 495, 495, 495, 495, 491, 355, 355, 355, 355, 491, 495, 495, 495, 355, 172, 355, 355, 355, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 355, 355, 355, 355, 491, 495, 495, 495, 495, 495, 491, 495, 495, 495, 495, 495, 495, 491, 491, 491, 491, 491, 491, 495, 495, 172, 172, 172, 355, 355, 355, 491, 355, 355, 355, 355, 355, 355, 355, 355, 355, 491, 355, 355, 355, 355, 491, 495, 495, 491, 495, 495, 491, 495, 495, 495, 491, 495, 495, 491, 495, 495, 495, 495, 355, 495, 495, 172, 172, 172, 355, 355, 355, 491, 355, 355, 355, 101, 101, 101, 355, 355, 355, 491, 355, 355, 355, 355, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 495, 495, 495, 355, 495, 495, 495, 355, 172, 355, 355, 355, 491, 355, 355, 355, 101, 101, 101, 355, 355, 355, 491, 355, 355, 355, 355, 491, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 491, 491, 495, 495, 495, 355, 355, 355, 355, 355, 172, 355, 355, 355, 491, 355, 355, 355, 101, 101, 101, 355, 355, 355, 491, 355, 355, 355, 355, 491, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 491, 495, 495, 495, 495, 355, 495, 495, 495, 355, 172, 355, 355, 355, 491, 355, 355, 355, 101, 101, 101, 355, 355, 355, 491, 355, 355, 355, 355, 491, 101, 101, 101, 101, 355, 355, 355, 355, 101, 101, 101, 101, 491, 491, 491, 491, 491, 491, 495, 495, 495, 355, 172, 355, 355, 355, 491, 355, 355, 355, 355, 355, 355, 355, 355, 355, 491, 355, 355, 355, 355, 491, 101, 101, 101, 101, 355, 355, 355, 355, 101, 101, 101, 101, 491, 355, 355, 355, 355, 491, 495, 495, 172, 172, 172, 355, 355, 355, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 355, 355, 355, 355, 491, 101, 101, 101, 101, 355, 355, 355, 355, 101, 101, 101, 101, 491, 355, 355, 355, 355, 491, 495, 495, 495, 355, 172, 355, 355, 355, 355, 355, 355, 355, 355, 491, 355, 355, 355, 355, 355, 355, 355, 355, 355, 491, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 491, 355, 355, 355, 355, 491, 495, 495, 495, 355, 172, 355, 355, 355, 355, 355, 355, 355, 355, 491, 355, 355, 355, 355, 355, 355, 355, 355, 355, 491, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 491, 355, 355, 355, 355, 491, 355, 355, 355, 355, 172, 355, 355, 355, 355, 355, 355, 355, 355, 491, 355, 355, 355, 355, 355, 355, 355, 355, 355, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 355, 355, 172, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 172, 355, 355, 355, 355, 355, 355, 355, 355, 355, 172, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 172, 355, 495, 495, 495, 355, 355, 495, 495, 495, 495, 355, 355, 172, 355, 495, 495, 495, 495, 495, 495, 495, 355, 172, 355, 495, 495, 495, 495, 495, 495, 495, 355, 495, 495, 495, 495, 495, 355, 355, 355, 355, 172, 491, 491, 495, 495, 355, 355, 495, 495, 495, 172, 172, 172, 172, 355, 495, 495, 495, 495, 495, 495, 495, 355, 172, 355, 495, 495, 495, 495, 495, 495, 495, 355, 495, 495, 495, 495, 495, 355, 355, 355, 355, 172, 355, 495, 495, 495, 355, 355, 495, 495, 495, 495, 355, 355, 172, 355, 495, 172, 495, 495, 495, 172, 495, 355, 172, 355, 495, 495, 495, 495, 172, 495, 495, 355, 495, 495, 172, 495, 495, 355, 355, 355, 355, 172, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 172, 355, 355, 172, 355, 355, 355, 172, 355, 355, 172, 355, 355, 355, 355, 355, 172, 355, 355, 355, 355, 355, 172, 355, 355, 355, 355, 355, 355, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 355, 355, 355, 355, 172, 491, 491, 491, 491, 491, 491, 172, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 172, 355, 355, 355, 172, 355, 355, 355, 355, 355, 172, 355, 355, 172, 355, 491, 355, 355, 355, 172, 355, 355, 355, 355, 172, 491, 495, 495, 495, 495, 491, 172, 355, 495, 495, 495, 355, 355, 495, 495, 495, 355, 172, 355, 495, 495, 172, 495, 355, 495, 495, 355, 172, 355, 495, 172, 495, 491, 495, 495, 355, 172, 355, 355, 355, 355, 172, 491, 495, 495, 495, 495, 491, 172, 355, 495, 495, 495, 355, 355, 495, 495, 172, 172, 172, 355, 495, 495, 495, 495, 355, 495, 495, 355, 172, 355, 495, 495, 495, 491, 495, 495, 355, 172, 355, 355, 355, 355, 172, 491, 495, 495, 495, 491, 491, 172, 172, 172, 495, 495, 355, 355, 495, 495, 495, 355, 172, 355, 355, 355, 355, 355, 355, 495, 172, 172, 172, 355, 355, 355, 355, 491, 495, 172, 172, 172, 355, 355, 355, 355, 172, 491, 495, 495, 495, 495, 491, 172, 355, 495, 495, 495, 355, 355, 495, 495, 495, 355, 172, 355, 495, 495, 495, 495, 355, 495, 495, 355, 172, 355, 495, 495, 495, 491, 495, 495, 355, 172, 355, 355, 355, 355, 172, 491, 495, 495, 495, 495, 491, 172, 355, 495, 495, 495, 355, 355, 495, 495, 495, 355, 172, 355, 495, 495, 172, 495, 355, 495, 495, 355, 172, 355, 495, 172, 495, 491, 495, 495, 355, 172, 355, 355, 355, 355, 172, 491, 491, 491, 491, 491, 491, 172, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 172, 355, 355, 355, 172, 355, 355, 355, 355, 355, 172, 355, 355, 172, 355, 491, 355, 355, 355, 172, 355, 355, 355, 355, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 355, 355, 355, 355, 172, 355, 355, 355, 355, 355, 355, 355, 355, 172, 355, 355, 355, 355, 491, 355, 172, 355, 491, 355, 355, 355, 491, 355, 172, 355, 355, 355, 172, 355, 355, 355, 355, 491, 355, 355, 355, 172, 355, 355, 355, 355, 172, 355, 495, 495, 495, 495, 495, 495, 355, 172, 355, 495, 495, 495, 491, 495, 172, 495, 491, 495, 495, 495, 491, 495, 172, 495, 355, 355, 172, 355, 495, 495, 495, 491, 495, 495, 355, 172, 355, 355, 355, 355, 172, 355, 495, 495, 172, 172, 495, 495, 355, 172, 355, 495, 172, 495, 491, 495, 495, 495, 491, 495, 172, 495, 491, 495, 495, 495, 355, 355, 172, 355, 495, 495, 495, 495, 495, 495, 355, 172, 355, 355, 355, 355, 172, 355, 355, 355, 172, 172, 355, 355, 355, 172, 355, 355, 172, 355, 491, 355, 355, 355, 491, 355, 172, 355, 491, 355, 355, 355, 355, 355, 172, 355, 355, 355, 355, 355, 355, 355, 355, 172, 355, 355, 355, 355, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 355, 355, 355, 355, 172, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 172, 355, 355, 355, 355, 355, 355, 491, 355, 355, 355, 355, 355, 355, 172, 491, 491, 491, 491, 172, 355, 355, 355, 355, 172, 355, 495, 495, 495, 495, 495, 495, 495, 495, 495, 495, 495, 495, 495, 495, 495, 355, 172, 355, 355, 355, 355, 355, 355, 491, 355, 355, 355, 355, 355, 355, 172, 491, 495, 495, 491, 172, 355, 355, 355, 355, 172, 355, 495, 172, 172, 172, 172, 172, 172, 172, 495, 495, 172, 172, 172, 495, 495, 355, 172, 355, 355, 355, 355, 355, 355, 491, 355, 355, 355, 355, 355, 355, 172, 491, 495, 495, 491, 172, 355, 355, 355, 355, 172, 355, 495, 172, 495, 495, 495, 495, 495, 172, 495, 495, 495, 495, 172, 495, 495, 355, 172, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 491, 172, 491, 491, 495, 491, 172, 355, 355, 355, 355, 172, 355, 495, 172, 172, 172, 172, 495, 495, 172, 172, 172, 172, 172, 172, 495, 495, 355, 172, 355, 355, 355, 355, 355, 355, 491, 355, 355, 355, 355, 355, 355, 172, 491, 495, 495, 491, 172, 355, 355, 355, 355, 172, 355, 495, 495, 495, 495, 172, 495, 495, 495, 495, 495, 495, 495, 495, 495, 495, 355, 172, 355, 355, 355, 355, 355, 355, 491, 355, 355, 355, 355, 355, 355, 172, 491, 495, 495, 491, 172, 355, 355, 355, 355, 172, 355, 355, 355, 355, 355, 172, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 172, 355, 355, 355, 355, 355, 355, 491, 355, 355, 355, 355, 355, 355, 172, 491, 491, 491, 491, 172, 355, 355, 355, 355, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 172, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355, 355
            };

            return new Map(terrain, characters);
        }

        private static List<Character> CreateRealCharacters()
        {
            var characters = new List<Character>
            {
                new Character() {Index = 0, Name = "Nick", Position = new Point(22, 18, Terrain.Asphalt)},
                new Character() {Index = 1, Name = "Steve", Position = new Point(4, 12, Terrain.Stones)},
                new Character() {Index = 2, Name = "Tony", Position = new Point(9, 8, Terrain.Grass)},
                new Character() {Index = 3, Name = "Hawkeye", Position = new Point(5, 34, Terrain.Earth)},
                new Character() {Index = 4, Name = "Hulk", Position = new Point(23, 37, Terrain.Asphalt)},
                new Character() {Index = 5, Name = "Thor", Position = new Point(35, 14, Terrain.Asphalt)},
                new Character() {Index = 6, Name = "Black Widow", Position = new Point(36, 36, Terrain.Stones)}
            };

            return characters;
        }

        private static bool IsNeighbor(Map map, Point point, Point a)
        {
            return map.GetNeighbors(point).Contains(a);
        }
    }
}
