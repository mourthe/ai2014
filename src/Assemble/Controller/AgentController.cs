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
        private readonly Map _map;
        private int _bugsFixed = 0;

        public AgentController(Map map)
        {
            _map = map;
        }

        public IList<string> Walk()
        {
            var actions = new List<string>();
            updatePerceptions(_map.Points[22,18]);
            while (_bugsFixed < 1)
            {
                unsafe {
                    try
                    {
                        var action = new Helper.Action(Prolog.BestMove(), this._map);
                        if (_bugsFixed == 10)
                        {
                            _map.RemoveVortex();
                        }
                        if (_bugsFixed == 14)
                        {
                            _map.RemoveCocks();
                        }
                        if (_bugsFixed == 18)
                        {
                            _map.RemoveHoles();
                        }

                        switch (action.move)
                        {
                            case BestMove.Attack:
                                _map.Points[action.point.I, action.point.J].HasCockroach = false;
                                //ENVIAR INFO DE BARATA MORTA
                                // ele pode atacar o lugar errado?
                                break;
                            case BestMove.MoveRight:
                                updatePerceptions(action.point);
                                break;
                            case BestMove.MoveLeft:
                                updatePerceptions(action.point);
                                break;
                            case BestMove.MoveUp:
                                updatePerceptions(action.point);
                                break;
                            case BestMove.MoveDown:
                                updatePerceptions(action.point);
                                break;
                            case BestMove.Debug:
                                break;
                            case BestMove.GetAmmo:
                                _map.Points[action.point.I, action.point.J].HasAmmo = false;
                                updatePerceptions(action.point);
                                break;
                            case BestMove.FixBug:
                                _bugsFixed++;
                                _map.Points[action.point.I, action.point.J].HasBug = false;
                                updatePerceptions(action.point);
                                break;
                            default:
                                break;
                        }

                        actions.Add(action.move.ToString());
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }

            return actions;
        }

        private void updatePerceptions(Point from)
        {
            Point up = null, down = null, left = null, right = null;

            if (from.I > 0)
            {
                up = _map.Points[from.I - 1, from.J];
            }

            if (from.I + 1 < 42)
            {
               down = _map.Points[from.I + 1, from.J];
            }

            if (from.J > 0)
            {
                left = _map.Points[from.I, from.J - 1];
            }

            if (from.J + 1 < 42)
            {
                right = _map.Points[from.I, from.J + 1];
            }


            bool hasShine = false, hasCockroach = false, hasBreeze = false, hasDistortions = false, hasBinaries = false;

            if (from.HasAmmo)
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

            if (from.HasBug)
                hasBinaries = true;
            
            unsafe
            {
                Prolog.UpdPerc(from.I, from.J, hasShine, hasCockroach, hasBreeze, hasDistortions, hasBinaries);
            }
        }
    }
}
