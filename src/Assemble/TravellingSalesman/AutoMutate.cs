using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAF;
using GAF.Operators;

namespace Operators
{
    public class AutoMutate : BinaryMutate
    {
        private AutoMutateFactor _autoMutationFactorS;
        private readonly object _syncLock = new object();
        private int _geneCount;

        public AutoMutate(double mutationProbability, bool allowDuplicates)
            : base(mutationProbability, allowDuplicates)
        {
        }

        public override void Invoke(Population currentPopulation, ref Population newPopulation,
          FitnessFunction fitnesFunctionDelegate)
        {
            _geneCount = newPopulation.ChromosomeLength;
            base.Invoke(currentPopulation, ref newPopulation, fitnesFunctionDelegate);
        }

        protected override void Mutate(Chromosome chromosome, double mutationProbability)
        {
            //adjust and scale for AutoMutate Factor based on the value of the last gene
            var newMutationProbability = 0.0;
            var nonPhenotypeGene = chromosome.Genes.ElementAt(_geneCount - 1);

            if (nonPhenotypeGene.BinaryValue == 1)
            {
                newMutationProbability = mutationProbability * (int)AutoMutationFactor;
            }
            else
            {
                newMutationProbability = mutationProbability;
            }

            base.Mutate(chromosome, newMutationProbability);

        }

        public AutoMutateFactor AutoMutationFactor
        {
            get
            {
                lock (_syncLock)
                {
                    return _autoMutationFactorS;
                }
            }
            set
            {
                lock (_syncLock)
                {
                    _autoMutationFactorS = value;
                }
            }
        }
    }
}
