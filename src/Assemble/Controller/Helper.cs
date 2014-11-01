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

        public class Action{
            public BestMove move;
            public Point point;
            public bool win;

            public unsafe Action(int* actionParameters){
                int i = 0;
                int[] actionParams = new int[5];

                while (actionParameters[i] != -1)
                {
                    actionParams[i] = actionParams[i];
                    i++;
                }
           
                if (actionParams != null && actionParams.Length >= 1)
                {
                    switch ((BestMove)actionParams[0])
                    {
                        case BestMove.Move:
                        case BestMove.AStar:
                        case BestMove.Debug:
                        case BestMove.GetAmmo:
                        case BestMove.FixBug:
                            point = new Point(actionParams[1], actionParams[2]);
                            break;
                        case BestMove.Attack:
                            point = new Point(actionParams[1], actionParams[2]);
                            break;

                    }
                }
                else {
                    throw new Exception("Não pude ler os parâmetros da action");
                }


            }
        } 
    }
}