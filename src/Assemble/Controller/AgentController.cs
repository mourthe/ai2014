using ManagedProlog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.Controller
{
    public class AgentController
    {
        private Map _map;
        private int BugsFixed = 0;

        public AgentController(Map map)
        {
            _map = map;
        }

        public IList<string> Walk()
        {
            var actions = new List<string>();
   
            while (BugsFixed < 20)
            {
                unsafe { 
                    var action = new Helper.Action(Prolog.BestMove()); 
                    if(BugsFixed == 10){
                        //remover vórtices
                    }
                    if(BugsFixed == 14){
                        //remover baratas
                    }
                    if(BugsFixed == 18){
                        //remover buracos
                    }

                    switch (action.move)
                    {
                        case BestMove.Attack:
                            _map.Points[action.point.I, action.point.J].HasCockroach = false;
                            //ENVIAR INFO DE BARATA MORTA
                            break;
                        case BestMove.Move: 
                            updatePerceptions(action.point); 
                            break;
                        case BestMove.Debug:
                            goto debug;
                            break;
                        case BestMove.GetAmmo:
                            updatePerceptions(action.point);
                            break;
                        case BestMove.FixBug:
                            BugsFixed++;
                            updatePerceptions(action.point);
                            break;
                        default:
                            break;
                    }

                    actions.Add(action.move.ToString());
                }
            }

            return actions;
        }

        private void updatePerceptions(Point from)
        {
            Point up = _map.Points[from.J - 1, from.I];
            Point down = _map.Points[from.J + 1, from.I];
            Point left = _map.Points[from.J, from.I - 1];
            Point right = _map.Points[from.J, from.I + 1];


            bool hasShine = false, hasCockroach = false, hasBreeze = false, hasDistortions = false, hasBinaries = false;
            
            if ((up != null && up.HasAmmo) || (down != null && down.HasAmmo)
                || (left != null && left.HasAmmo) || (right != null && right.HasAmmo))
                hasShine = true;

            if ((up != null && up.HasCockroach) || (down != null && down.HasCockroach)
               || (left != null && left.HasCockroach) || (right != null && right.HasCockroach))
                hasCockroach = true;

            if ((up != null && up.HasHole) || (down != null && down.HasHole)
               || (left != null && left.HasHole) || (right != null && right.HasHole))
                hasBreeze = true;

            if ((up != null && up.HasVortex) || (down != null && down.HasVortex)
                || (left != null && left.HasVortex) || (right != null && right.HasVortex))
                hasDistortions = true;

            if ((up != null && up.HasBug) || (down != null && down.HasBug)
                || (left != null && left.HasBug) || (right != null && right.HasBug))
                hasBinaries = true;


            unsafe
            {
                Prolog.UpdPerc(from.J, from.I, hasShine, hasCockroach, hasBreeze, hasDistortions, hasBinaries);
            }
        }
    }
}
