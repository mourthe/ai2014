using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagedProlog;

namespace Assemble.Controller
{
    public class Helper
    {
        public static void InitializeProlog(string path)
        {
            unsafe
            {
                Prolog.Initilize(Helper.StrToSbt(path));
            }
        }

        public static void PutCockroach(int x, int y)
        {
            // Prolog.PutCockroach(x, y);
        }

        public static void PutAmmo(int x, int y)
        {
            // Prolog.PutAmmo(x, y);
        }

        public static void PutBug(int x, int y)
        {
            // Prolog.PutBug(x, y);
        }

        public static void PutVortex(int x, int y)
        {
            // Prolog.PutVortex(x, y);
        }

        public unsafe static sbyte* StrToSbt(string str)
        {
            var bytes = Encoding.ASCII.GetBytes(str);

            fixed (byte* p = bytes)
            {
                var sp = (sbyte*)p;
                return sp;
            }
        }
    }
}