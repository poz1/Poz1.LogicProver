using System;
using System.Collections.Generic;

namespace Poz1.LogicProver.Model.MGU
{
    public class MostGeneralUnifier : IMostGeneralUnifier
    {
        private readonly List<TerminalEquation> equations = new List<TerminalEquation>();

        public void AddEquation(Terminal t1, Terminal t2)
        {
            //Rule 3 [x = x -> Cancel Equation]
            if (t1.Value == t2.Value)
                return;

            //Rule 4 [t = x -> x = t]
            if (t1.GetType() == typeof(Variable) && t2.GetType() != typeof(Variable))
                equations.Add(new TerminalEquation(t2, t1));
            else
                equations.Add(new TerminalEquation(t1, t2));
        }

        public void AddEquation(Function f1, Function f2)
        {
            //Rule 2 [f!=g || different number of params -> Fail]
            if (f1.Arity != f2.Arity)
                throw new Exception("No MGU: different arity");
            if (f1.Value != f2.Value)
                throw new Exception("No MGU: different function");

            //Rule 1 [propagation to function terms]
            for (int j = 0; j < f1.Arity; j++)
            {
                AddEquation(f1.Parameters[j], f2.Parameters[j]);
            }
        }

        public Substitution Compute()
        {
            var solution = new Substitution();

            foreach (var eq in equations)
            {
                if (eq.terminal1.GetType() == typeof(Function) && eq.terminal2.GetType() == typeof(Function))
                {
                    var f1 = (Function)eq.terminal1;
                    var f2 = (Function)eq.terminal2;

                    if (f1.Arity != f2.Arity)
                        throw new Exception("No MGU: different arity");
                    if (f1.Value != f2.Value)
                        throw new Exception("No MGU: different function");

                    for (int j = 0; j < f1.Arity; j++)
                    {
                        equations.Add(new TerminalEquation(f1.Parameters[j], f2.Parameters[j]));
                    }
                }

                if (eq.terminal1.Value == eq.terminal2.Value)
                    equations.Remove(eq);


            }

            return null;
        }
    }
}
