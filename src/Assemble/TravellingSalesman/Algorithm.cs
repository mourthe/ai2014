namespace TravellingSalesman
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GAF;
    using GAF.Extensions;
    using GAF.Operators;
    using Assemble;

    /// <summary>
    /// This is a simple example of how the GAF can be configured to 
    /// solve the Travelling Salesman problem. This example is not mean't
    /// to be a fully optimised solution, but simply something to start the
    /// process with.
    /// 
    /// 
    /// </summary>
    internal class Algorithm
    {
        private const int genesCount = 6;
        private static void Execute(Map map, IList<Character> chars)
        {
            //Each city can be identified by an integer within the range 0-15
            //our chromosome is a special case as it needs to contain each city 
            //only once. Therefore, our chromosome will contain all the integers
            //between 0 and 15 with no duplicates

            //16 in our population so create the population
            var population = new Population(100);

            for (var p = 0; p < 100; p++)
            {
                var chromosome = new Chromosome();
                for (var g = 0; g < 16; g++)
                {
                    chromosome.Genes.Add(new Gene(g));
                }
                chromosome.Genes.Shuffle();
                population.Solutions.Add(chromosome);
            }

            var elite = new Elite(5);
            var crossover = new Crossover(0.8)
                {
                    CrossoverType = CrossoverType.DoublePointOrdered
                };

            var mutate = new SwapMutate(0.02);

            //run the GA
            var ga = new GeneticAlgorithm(population, CalculateFitness);

            ga.OnGenerationComplete += ga_OnGenerationComplete;
            ga.OnRunComplete += ga_OnRunComplete;
            ga.Operators.Add(elite);
            ga.Operators.Add(crossover);
            ga.Operators.Add(mutate);
            ga.Run(Terminate);

        }

        static void ga_OnRunComplete(object sender, GaEventArgs e)
        {
            var fittest = e.Population.GetTop(1)[0];
            foreach (var gene in fittest.Genes)
            {
                Console.WriteLine(_cities[(int)gene.RealValue].Name);
            }
        }

        private static void ga_OnGenerationComplete(object sender, GaEventArgs e)
        {
            var fittest = e.Population.GetTop(1)[0];
            var distanceToTravel = CalculateDistance(fittest);
            Console.WriteLine(string.Format("Generation: {0}, Fitness: {1}, Distance: {2}", e.Generation, fittest.Fitness, distanceToTravel));
                 
        }

        private static IEnumerable<City> CreateCities()
        {
            var cities = new List<City>();
            cities.Add(new City("Birmingham", 52.486125, -1.890507));
            cities.Add(new City("Bristol", 51.460852, -2.588139));
            cities.Add(new City("London", 51.512161, -0.116215));
            cities.Add(new City("Leeds", 53.803895, -1.549931));
            cities.Add(new City("Manchester", 53.478239, -2.258549));
            cities.Add(new City("Liverpool", 53.409532, -3.000126));
            cities.Add(new City("Hull", 53.751959, -0.335941));
            cities.Add(new City("Newcastle", 54.980766, -1.615849));
            cities.Add(new City("Carlisle", 54.892406, -2.923222));
            cities.Add(new City("Edinburgh", 55.958426, -3.186893));
            cities.Add(new City("Glasgow", 55.862982, -4.263554));
            cities.Add(new City("Cardiff", 51.488224, -3.186893));
            cities.Add(new City("Swansea", 51.624837, -3.94495));
            cities.Add(new City("Exeter", 50.726024, -3.543949));
            cities.Add(new City("Falmouth", 50.152266, -5.065556));
            cities.Add(new City("Canterbury", 51.289406, 1.075802));

            return cities;
        }

        public static double CalculateFitness(Chromosome chromosome)
        {
            //X' = X * k + d; 
            //k = (B' - A') / (A - B);
            //d = A' - B * k;
            //k = -1/50
            //d = 5
            //x' = x * (-1 / 50) + 5;

            var distanceToTravel = CalculateDistance(chromosome);
            return 1 - distanceToTravel / 10000;
        }
    
        private static double CalculateDistance(Chromosome chromosome)
        {
            var distanceToTravel = 0.0;
            City previousCity = null;

            //run through each city in the order specified in the chromosome
            foreach (var gene in chromosome.Genes)
            {
                var currentCity = _cities[(int) gene.RealValue];

                if (previousCity != null)
                {
                    var distance = previousCity.GetDistanceFromPosition(currentCity.Latitude,
                                                                        currentCity.Longitude);

                    distanceToTravel += distance;
                    //Console.WriteLine(string.Format("Chromosome: {0} {1}Km between {2} and {3}.",chromosome.Id, distance, currentCity.Name, previousCity.Name));
                }

                previousCity = currentCity;
            }

            return distanceToTravel;
        }

        public static bool Terminate(Population population, int currentGeneration, long currentEvaluation)
        {
            return currentGeneration > 500;
        }

    }
}
