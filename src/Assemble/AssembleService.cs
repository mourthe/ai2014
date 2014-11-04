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
        public static IList<string> GetBestPath(IList<int> terrain, IList<int> content, out List<int> cost)
        {
            return new Map(terrain, content).FixBugs(out cost);
        }

    }
}
