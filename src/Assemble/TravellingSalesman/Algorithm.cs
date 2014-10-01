using System.Runtime.CompilerServices;

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
        private const int popCount = 200;
        private const double crossoverProb = 1.0;
        private static List<Character> _characters;
        private static Map _cMap;

        public static List<string> Execute(Map map)
        {
            // save the map for further use 
            _cMap = map;

            //Each character can be identified by an integer within the range 0-5
            //our chromosome is a special case as it needs to contain each city 
            //only once. Therefore, our chromosome will contain all the integers
            //between 0 and 5 with no duplicates

            // {{1,2,3,4,5,6},{1,2,4,3,5,6} ... }
            var population = new Population(popCount);
            for (var p = 0; p < popCount; p++)
            {
                var chromosome = new Chromosome();
                for (var g = 1; g <= genesCount; g++)
                {
                    chromosome.Genes.Add(new Gene(g));
                }
                chromosome.Genes.Shuffle();
                population.Solutions.Add(chromosome);
            }   

            var elite = new Elite(10);
            var crossover = new Crossover(crossoverProb)
                {
                    CrossoverType = CrossoverType.DoublePointOrdered
                };

            var mutate = new SwapMutate(0.5);

            //run the GA
            var ga = new GeneticAlgorithm(population, CalculateFitness);

            ga.OnGenerationComplete += ga_OnGenerationComplete;
            ga.OnRunComplete += ga_OnRunComplete;
            ga.Operators.Add(elite);
            ga.Operators.Add(crossover);
            ga.Operators.Add(mutate);
            ga.Run(Terminate);

            var names = new List<string>();
            var fittest = ga.Population.GetTop(1)[0];
            foreach (var gene in fittest.Genes)
            {
                names.Add(_cMap.Characters.FirstOrDefault(c => c.Index == (int)gene.RealValue).Name);
            }

            return names;

        }

        static void ga_OnRunComplete(object sender, GaEventArgs e)
        {
             var fittest = e.Population.GetTop(1)[0];
            foreach (var gene in fittest.Genes)
            {
                Console.WriteLine(_cMap.Characters.FirstOrDefault(c => c.Index == (int)gene.RealValue).Name);
            }

        }

        private static void ga_OnGenerationComplete(object sender, GaEventArgs e)
        {
            var fittest = e.Population.GetTop(1)[0];
            var distanceToTravel = CalculateDistance(fittest);
            Console.WriteLine(string.Format("Generation: {0}, Fitness: {1}, Distance: {2}", e.Generation, fittest.Fitness, distanceToTravel));
                 
        }

        private static double CalculateFitness(Chromosome chromosome)
        {
            var distance = CalculateDistance(chromosome);
            return distance;
        }
    
        private static double CalculateDistance(Chromosome chromosome)
        {
            var distanceToTravel = 0.0;
            int currentPoint;
            int destinationPoint;

            currentPoint = 0;
            destinationPoint = (int)chromosome.Genes[0].RealValue;
            distanceToTravel += _cMap.Result[currentPoint, destinationPoint].Cost;

            for (var i = 0; i < chromosome.Genes.Count - 1; i++)
            {
                try
                {
                    currentPoint = (int)chromosome.Genes[i].RealValue;
                    destinationPoint = (int)chromosome.Genes[i + 1].RealValue;

                    distanceToTravel += _cMap.Result[currentPoint, destinationPoint].Cost;
                }
                catch (Exception e)
                {
                    Console.Out.WriteLine(e);
                }
            }

            currentPoint = (int)chromosome.Genes[chromosome.Genes.Count - 1].RealValue;
            destinationPoint = 0;
            distanceToTravel += _cMap.Result[currentPoint, destinationPoint].Cost;

            return distanceToTravel;
        }

        public static bool Terminate(Population population, int currentGeneration, long currentEvaluation)
        {
            return currentGeneration > 100;
        }

    }
}
