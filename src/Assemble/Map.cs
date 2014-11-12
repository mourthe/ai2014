using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Assemble.AStar;
using Assemble.Controller;
using ManagedProlog;

namespace Assemble
{
    public class Map
    {
        public Point[,] Points { get; set; }
        public IList<Character> Characters { get; set; }
        public SearchResult[,] Result { get; set; }

        public static readonly int Height = 42;
        public static readonly int Width = 42;
        private readonly int _size;
        public List<Character> CharactersWithNick { get; private set; }

        public Map(IList<int> terrain, IList<int> elements, int size = 42) 
        {
            try
            {
                unsafe { Prolog.Initilize(Helper.StrToSbt(@"C:\Projects\ai2014\Prolog\rules.pl")); }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            _size = size;
            this.Result = null;
            this.Points = new Point[_size, _size];
            this.BuildTerrain(terrain, elements);
            
            // copia charactersWithNick e o tira da lista
            // this.CharactersWithNick = characters.ToList();
            // characters.RemoveAt(0);

            //this.Characters = this.CreateCharacters(characters);
            // this.InitializeResult();
            // Mapa tá pronto, vc tem o mapa!
        }

        private IList<Character> CreateCharacters(IList<Character> characters)
        {
            int numberOfavengers = 0; 
            Random random = new Random(Guid.NewGuid().GetHashCode());

            // fill the terrain atribute on characters
            foreach (var character in characters)
            {
                character.Position.Terrain = Points[character.Position.I, character.Position.J].Terrain;
            }

            // fill the terrain atribute on charactersWithNick
            foreach (var character in CharactersWithNick)
            {
                character.Position.Terrain = Points[character.Position.I, character.Position.J].Terrain;
            }

            while (numberOfavengers < 3)
            {
                foreach (var character in characters)
                {
                    if (numberOfavengers < 3 && random.NextDouble() > 0.5 && !character.isConvincible)
                    {
                        character.isConvincible = true;
                        numberOfavengers++;
                    }
                }
            }

            return characters;
        }

        public IList<string> FixBugs(out List<int> cost)
        { 
            var agent = new AgentController(this);
            var actions = agent.Walk();

            cost = actions.Select(GetActionCost).ToList();

            return GetActionsInDirections(actions);
        }

        private static IList<string> GetActionsInDirections(IEnumerable<string> actions)
        {
            var directions = new List<string>();

            foreach (var action in actions)
            {
                switch (action)
                {
                    case "MoveUp": directions.Add("n"); break;
                    case "MoveDown": directions.Add("s"); break;
                    case "MoveLeft": directions.Add("w"); break;
                    case "MoveRight": directions.Add("e"); break;
                    case "Attack": directions.Add("attack"); break;
                    
                    default: directions.Add("stop");
                        break;
                }
            }

            return directions;
        }

        public int GetActionCost(string action)
        {
            return (int)((MoveCosts)System.Enum.Parse(typeof(MoveCosts), action, true));
        }

        private void BuildTerrain (IList<int> terrain, IList<int> content ){
            for (int x = 0, i = 0; i < _size; i++)
            {
                for (var j = 0; j < _size; x++, j++)
                {
                    this.Points[i, j] = new Point(i, j, content.ElementAt(x));
                    Helper.PutTerrain(i, j, terrain.ElementAt(x));
                } 
            }
        }

        
        public IList<string> GetBestPath(out double cost, out IList<string> party)
        {
            if (this.Result == null)
            {
                this.InitializeResult();
            }

            var names = TravellingSalesman.Algorithm.Execute(this/*,out cost*/);
            party = GetThreeConvincedNames(names);
            return this.GetPathInDirections(party.ToList(), out cost);
        }

        public IList<Point> GetNeighbors(Point point)
        {
            var neighbors = new List<Point>();

            // não está na primeira coluna e o vizinho não é predio
            if (point.J != 0 && Points[point.I, point.J - 1].Terrain != Terrain.Building)
                neighbors.Add(Points[point.I, point.J - 1]);

            // não está na ultima coluna e o vizinho não é predio
            if (point.J != _size - 1 && Points[point.I, point.J + 1].Terrain != Terrain.Building)
                neighbors.Add(Points[point.I, point.J + 1]);

            // não está na primeira linha e o vizinho não é predio
            if (point.I != 0 && Points[point.I - 1, point.J].Terrain != Terrain.Building)
                neighbors.Add(Points[point.I - 1, point.J]);

            // não está na ultima linha e o vizinho não é predio
            if (point.I != _size - 1 && Points[point.I + 1, point.J].Terrain != Terrain.Building)
                neighbors.Add(Points[point.I + 1, point.J]);

            return neighbors;
        }

        private IList<string> GetPathInDirections(List<string> names, out double cost)
        {
            cost = 0;
            var currPos = new Point(22, 18, this.Points[22, 18].Terrain);
            var steps = new List<string>();
            names.Add("Nick");
            foreach (var name in names)
            {
                try
                {
                    var dest = GetPointFromName(name);
                    var path = GetPathInPoints(currPos, dest, ref cost);

                    var stepDests = path as Point[] ?? path.ToArray();
                    foreach (var stepDest in stepDests)
                    {
                        steps.Add(GetStep(currPos, stepDest));
                        currPos = stepDest;
                    }

                    //Adiciona step de stop para poder rolar a animação da conversa de convencimento
                    steps.Add("stop");
                    currPos = stepDests.Last();
                }
                catch (Exception e)
                {                    
                    throw e;
                }
            }
            return steps;
        }

        private static string GetStep(Point currPos, Point dest)
        {
            if (dest.I > currPos.I)
            {
                return "s";
            }

            if (dest.I < currPos.I)
            {
                return "n";
            }

            if (dest.J > currPos.J)
            {
                return "e";
            }

            if (dest.J < currPos.J)
            {
                return "w";
            }

            return "stop";
        }

        private IEnumerable<Point> GetPathInPoints(Point currPos, Point dest, ref double cost)
        {
            int current = 0, destination = 0;
            
            // checa se esta nos characters
            for (int i = 0, j = 0; i < CharactersWithNick.Count; i++, j++)
            {
                if (currPos.Equals(CharactersWithNick[i].Position))
                {
                    current = i;
                }

                if (dest.Equals(CharactersWithNick[j].Position))
                {
                    destination = j;
                }
            }

            cost += this.Result[current, destination].Cost;
            
            return this.Result[current, destination].BestPath;
        }

        private Point GetPointFromName(string name)
        {
            return CharactersWithNick.FirstOrDefault(c => c.Name == name).Position;
        }

        private List<string> GetThreeConvincedNames(IEnumerable<string> names)
        {
            var count = 0;
            var filteredNames = new List<string>();
            
            foreach (var name in names)
            {
                // add the name
                filteredNames.Add(name);

                if (this.Characters.FirstOrDefault(c => c.Name == name).isConvincible)
                {
                    count++;
                    // if got 3 convicibles breack
                    
                    if (count == 3)
                    {
                        break;
                    }
                }
            }

            return filteredNames;
        }
        
        public void InitializeResult()
        {
            var result = new SearchResult[7, 7];
            var aStar = new AStar.AStar(this);

            for (var i = 0; i < CharactersWithNick.Count; i++)
            {
                for (var j = i; j < CharactersWithNick.Count; j++)
                {
                    if (j != i)
                    {
                        result[i, j] = aStar.Star(CharactersWithNick[i].Position, CharactersWithNick[j].Position);
                        result[j, i] = aStar.Star(CharactersWithNick[j].Position, CharactersWithNick[i].Position);

                        // matriz é simétrica em relação a diagonal
                        // result[j, i] = new SearchResult(result[i, j].Cost, result[i, j].BestPath.ToList());
                        // result[j, i].BestPath.Reverse();
                    }
                    else
                    {
                        result[i, j] = new SearchResult(0, null);
                    }
                }
            }

            this.Result = result;
        }

        public IEnumerable<Point> GetVisitedNeighborhood(Point point, Point final)
        {
            var retInxs = new List<Point>();

            if ((new Point(point.I + 1, point.J)).Equals(final) || Prolog.IsVisited(point.I + 1, point.J))
                retInxs.Add(new Point(point.I + 1, point.J)); 

            if ((new Point(point.I - 1, point.J)).Equals(final) || Prolog.IsVisited(point.I - 1, point.J))
                retInxs.Add(new Point(point.I - 1, point.J));

            if ((new Point(point.I, point.J + 1)).Equals(final) || Prolog.IsVisited(point.I, point.J + 1))
                retInxs.Add(new Point(point.I, point.J + 1)); 

            if ((new Point(point.I, point.J - 1)).Equals(final) || Prolog.IsVisited(point.I, point.J - 1))
                retInxs.Add(new Point(point.I, point.J - 1));

            return retInxs;
        }

        public IEnumerable<Point> GetSafeNeighborhood(Point point)
        {
            var retInxs = new List<Point>();
            
            if (point.I + 1 < 42 && Prolog.IsSafe(point.I + 1, point.J))
                retInxs.Add(new Point(point.I + 1, point.J)); 

            if (point.I - 1 >= 0 && Prolog.IsSafe(point.I - 1, point.J))
                retInxs.Add(new Point(point.I - 1, point.J)); 

            if (point.J + 1 < 42 && Prolog.IsSafe(point.I, point.J + 1))
                retInxs.Add(new Point(point.I, point.J + 1)); 

            if (point.J - 1 >= 0 && Prolog.IsSafe(point.I, point.J - 1))
                retInxs.Add(new Point(point.I, point.J - 1));

            return retInxs;
        }

        public void RemoveVortex()
        {
            for (var i = 0; i < this.Points.Length; i++)
            {
                for (var j = 0; j < this.Points.Length; j++)
                {
                    this.Points[i, j].HasVortex = false;
                }
            }
        }

        public void RemoveCocks()
        {
            for (var i = 0; i < this.Points.Length; i++)
            {
                for (var j = 0; j < this.Points.Length; j++)
                {
                    this.Points[i, j].HasCockroach = false;
                }
            }
        }

        public void RemoveHoles()
        {
            for (var i = 0; i < this.Points.Length; i++)
            {
                for (var j = 0; j < this.Points.Length; j++)
                {
                    this.Points[i, j].HasHole = false;
                }
            }
        }
    }
}
