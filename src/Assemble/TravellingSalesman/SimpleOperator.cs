using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAF;

namespace Operators
{
    public class SimpleOperator : IOperator
    {
        public void Invoke(Population currentPopulation, ref Population newPopulation, FitnessFunction fitnesFunctionDelegate)
        {
            throw new NotImplementedException();
        }

        public int GetOperatorInvokedEvaluations()
        {
            throw new NotImplementedException();
        }
    }
}
