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
        private Point _currentPoint;

        public AgentController(Map map)
        {
            _map = map;
            _currentPoint = _map.Points[22, 18];
        }

        public IList<string> Walk()
        {
            IList<string> actions = new List<string>();
            updatePerceptions(_currentPoint);
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
                    
                        this.UpdateCurrentPoint(action);
                        
                        switch (action.move)
                        {
                            case BestMove.Attack:
                                _map.Points[action.point.I, action.point.J].HasCockroach = false;
                                //ENVIAR INFO DE BARATA MORTA
                                // ele pode atacar o lugar errado?
                                break;
                            case BestMove.MoveUp:
                                updatePerceptions(action.point);
                                break;
                            case BestMove.MoveDown:
                                updatePerceptions(action.point);
                                break;
                            case BestMove.MoveRight:
                                updatePerceptions(action.point);
                                break;
                            case BestMove.MoveLeft:
                                updatePerceptions(action.point);
                                break;
                            case BestMove.AStar:
                                var result = new AStar.AStar(_map).Star(_currentPoint, action.point);
                                NickFromTo(result.BestPath, ref actions);
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

        private void NickFromTo(IEnumerable<Point> bestPath, ref IList<string> action )
        {
            var currPos = _currentPoint;
            foreach (var point in bestPath)
            {
                action.Add(GetStep(currPos, point).ToString());
                currPos = point;
            }
        }

        private static BestMove GetStep(Point currPos, Point dest)
        {
            if (dest.I > currPos.I)
            {
                return BestMove.MoveDown;
            }

            if (dest.I < currPos.I)
            {
                return BestMove.MoveUp;
            }

            return dest.J > currPos.J ? BestMove.MoveRight : BestMove.TurnLeft;
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

        private void UpdateCurrentPoint(Helper.Action action)
        {
            if (action.move == BestMove.MoveUp || action.move == BestMove.MoveDown ||
                action.move == BestMove.MoveLeft || action.move == BestMove.MoveRight)
            {
                _currentPoint = action.point;
            }
        }
    }
}
