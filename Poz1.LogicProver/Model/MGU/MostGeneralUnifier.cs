using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
            if (t1.GetType() == typeof(VariableTerminal) && t2.GetType() != typeof(VariableTerminal))
                equations.Add(new TerminalEquation(t2, t1));
            else
                equations.Add(new TerminalEquation(t1, t2));
        }

        public void AddEquation(FunctionTerminal f1, FunctionTerminal f2)
        {
            //Rule 2 [f!=g || different number of params -> Fail]
            if (f1.Arity != f2.Arity)
                throw new Exception("No MGU: different arity");
            if (f1.Value != f2.Value)
                throw new Exception("No MGU: different function");

            //Rule 1 [propagation to function terms]
            for (int j = 0; j < f1.Arity; j++)
            {
                var f1p = f1.Parameters[j];
                var f2p = f2.Parameters[j];

                if(f1p is FunctionTerminal terminal1 && f2p is FunctionTerminal terminal2)
                    AddEquation(terminal1, terminal2);
                else
                    AddEquation(f1p, f2p);
            }
        }
       
        public Substitution Compute()
        {
            foreach (var eq in equations)
            {
                if(eq.Terminal1 is FunctionTerminal functionTerminal)
                {
                    if (functionTerminal.Parameters.Any(x => x.Value == eq.Terminal2.Value))
                    {
                        //Rule 6
                        throw new Exception("No MGU: variable in function [occur check]");
                    }
                    else
                    {
                        solution.Add(eq.Terminal1, eq.Terminal2);
                        foreach (var otherEq in equations)
                        {
                            if(otherEq != eq && otherEq.Terminal2.Value == eq.Terminal2.Value)
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
