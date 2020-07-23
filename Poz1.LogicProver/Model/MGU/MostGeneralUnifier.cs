using Poz1.LogicProver.Model.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Poz1.LogicProver.Model.MGU
{
    //[Martelli, Montanari, 1982]
    public class MostGeneralUnifier : IMostGeneralUnifier
    {
        //public static MostGeneralUnifier Instance;

        private readonly List<Equation<LogicElement>> equations = new List<Equation<LogicElement>>();
        public void AddEquation(ILogicElement x1, ILogicElement x2)
        {
            LogicElement t1 = x1.ToLogicElement();
            LogicElement t2 = x2.ToLogicElement();

            if (t1 is Function f1 && t2 is Function f2)
                AddFunctionEquation(f1, f2);

            //Rule 3 [x = x -> Cancel Equation]
            if (t1.Name == t2.Name)
                return;

            //Rule 4 [t = x -> x = t]
            if (t1.GetType() == typeof(Variable) && t2.GetType() != typeof(Variable))
                equations.Add(new Equation<LogicElement>(t2, t1));
            else
                equations.Add(new Equation<LogicElement>(t1, t2));
        }

        public void AddFunctionEquation(Function f1, Function f2)
        {
            //Rule 2 [f!=g || different number of params -> Fail]
            if (f1.Arity != f2.Arity)
                throw new Exception("No MGU: different arity");
            if (f1.Name != f2.Name)
                throw new Exception("No MGU: different function");

            //Rule 1 [propagation to function terms]
            for (int j = 0; j < f1.Arity; j++)
            {
                var f1p = f1.Parameters[j];
                var f2p = f2.Parameters[j];

                if(f1p is Function terminal1 && f2p is Function terminal2)
                    AddEquation(terminal1, terminal2);
                else
                    AddEquation(f1p, f2p);
            }
        }

        public void Clear()
        {
            equations.Clear();
        }

        public Substitution Unify(IList<Equation<LogicElement>> equations)
        {
            foreach (var eq in equations)
            {
                AddEquation(eq.Terminal1, eq.Terminal2);
            }

            return Compute();
        }

        public Substitution Compute()
        {
            foreach (var eq in equations)
            {
                if(eq.Terminal1 is Function functionTerminal)
                {
                    if (functionTerminal.Parameters.Any(x => x.Name == eq.Terminal2.Name))
                    {
                        //Rule 6
                        throw new Exception("No MGU: variable in function [occur check]");
                    }
                    else
                    {
                        //Rule 5
                        foreach (var otherEq in equations)
                        {
                            if(otherEq != eq && otherEq.Terminal2.Name == eq.Terminal2.Name)
                            {
                                otherEq.Terminal2 = eq.Terminal1;
                            }
                        }
                    }
                }
            }

            return new Substitution(equations);
        }
    }
}
