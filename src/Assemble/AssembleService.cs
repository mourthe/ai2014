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
        public IList<string> GetBestPath(IList<int> terrain, IList<Character> characters)
        {
            return new Map(terrain, characters).GetBestPath();
        }
    }
}
