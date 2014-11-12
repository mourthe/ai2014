using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble
{
    public class AssembleService
    {
        public static IList<Action> GetBestPath(IList<int> terrain, IList<int> content)
        {
            return new Map(terrain, content).FixBugs();
        }

    }
}
