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
        public static IList<string> GetBestPath(IList<int> terrain, IList<Character> characters, out double cost, out IList<string> party)
        {
            return new Map(terrain, characters).GetBestPath(out cost, out party);
        }
    }
}
