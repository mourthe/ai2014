﻿using ManagedProlog;
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
   
            while (_bugsFixed < 20)
            {
                unsafe {
                    try
                    {
                        var action = new Helper.Action(Prolog.BestMove());
                        if (_bugsFixed == 10)
                        {
                            RemoveVortex();
                        }
                        if (_bugsFixed == 14)
                        {
                            RemoveCocks();
                        }
                        if (_bugsFixed == 18)
                        {
                            RemoveHoles();
                        }

                        switch (action.move)
                        {
                            case BestMove.Attack:
                                _map.Points[action.point.I, action.point.J].HasCockroach = false;
                                //ENVIAR INFO DE BARATA MORTA
                                // ele pode atacar o lugar errado?
                                break;
                            case BestMove.Move:
                                updatePerceptions(action.point);
                                break;
                            case BestMove.Debug:
                                break;
                            case BestMove.GetAmmo:
                                updatePerceptions(action.point);
                                break;
                            case BestMove.FixBug:
                                _bugsFixed++;
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

        private void RemoveVortex()
        {
            for (var i = 0; i < _map.Points.Length; i++)
            {
                for (var j = 0; j < _map.Points.Length; j++)
                {
                    _map.Points[i, j].HasVortex = false;
                }
            }
        }

        private void RemoveCocks()
        {
            for (var i = 0; i < _map.Points.Length; i++)
            {
                for (var j = 0; j < _map.Points.Length; j++)
                {
                    _map.Points[i, j].HasCockroach = false;
                }
            }
        }

        private void RemoveHoles()
        {
            for (var i = 0; i < _map.Points.Length; i++)
            {
                for (var j = 0; j < _map.Points.Length; j++)
                {
                    _map.Points[i, j].HasHole = false;
                }
            }
        }
    }
}
