using Poz1.LogicProver.Model.World;
using System;
using System.Collections.Generic;

namespace Poz1.LogicProver.Model.Core
{
    public abstract class Formula
    {    
        public IList<WorldSymbol> WorldIndex { get; set; }

        public abstract IList<VariableTerminal> FreeVariables { get; }
    }

    public class AtomicFormula : Formula
    {
        public string Predicate { get; set; }
        public IList<Terminal> Parameters { get; set; }
        public int Arity => Parameters.Count;

        public override IList<VariableTerminal> FreeVariables => ComputeFreeVariables();

        private IList<VariableTerminal> ComputeFreeVariables()
        {
            var vars = new List<VariableTerminal>();
            foreach (var terminal in Parameters)
            {
                vars.AddRange(terminal.Variables);
            }

            return vars;
        }

        //[Martelli, Montanari, 1982]
        public Substitution Unify(AtomicFormula formula)
        {
            throw new NotImplementedException();
            //var equations = new List<TerminalEquation>();

            //for (int i = 0; i < Parameters.Count; i++)
            //{
            //    var t1 = Parameters[i];
            //    var t2 = formula.Parameters[i];

                

            //    else
            //        equations.Add(new TerminalEquation(t1, t2));
            //}

            //foreach(var eq in equations)
            //{
            //    if(eq.terminal1.Value == eq.terminal2.Value) { }
            //    //remove from list{

            //    //IF t1 is func or const e t2 is x and x is not param of func i can replace x with func or const

            //    //if is part it fails
            //}

            //var sub = new Substitution();
            //for (int i = 0; i < Parameters.Count; i++)
            //{
            //    var terminal = Parameters[i];
            //    terminal.FindUnification(sub, formula.Parameters[i]);
            //}
            //return sub;
        }
    }

    public class UnaryFormula : Formula
    {
        public UnaryConnective Connective { get; set; }
        public Formula Formula { get; set; }

        public override IList<VariableTerminal> FreeVariables => Formula.FreeVariables;
    }

    public class BinaryFormula : Formula
    {
        public BinaryConnective Connective { get; set; }
        public Formula LHSFormula { get; set; }
        public Formula RHSFormula { get; set; }

        public override IList<VariableTerminal> FreeVariables => ComputeFreeVariables();

        private IList<VariableTerminal> ComputeFreeVariables()
        {
            var vars = new List<VariableTerminal>();
            vars.AddRange(LHSFormula.FreeVariables);
            vars.AddRange(RHSFormula.FreeVariables);
            return vars;
        }
    }

    public class QuantifierFormula : Formula
    {
        public Quantifier Quantifier { get; set; }
        public VariableTerminal Variable { get; set; }
        public Formula Formula { get; set; }

        public override IList<VariableTerminal> FreeVariables => ComputeFreeVariables();

        private IList<VariableTerminal> ComputeFreeVariables()
        {
            var vars = new List<VariableTerminal>();
            vars.AddRange(Formula.FreeVariables);
            vars.Remove(Variable);
            return vars;
        }
    }
}
