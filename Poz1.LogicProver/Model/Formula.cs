using Poz1.LogicProver.Model.World;
using System;
using System.Collections.Generic;

namespace Poz1.LogicProver.Model
{
    public abstract class Formula
    {    
        public IList<WorldSymbol> WorldIndex { get; set; }

        public abstract IList<Variable> FreeVariables { get; }

        public Substitution Unify(Formula formula)
        {
            throw new NotImplementedException();
        }
    }

    public class AtomicFormula : Formula
    {
        public string Predicate { get; set; }
        public IList<Terminal> Parameters { get; set; }
        public int Arity => Parameters.Count;

        public override IList<Variable> FreeVariables => ComputeFreeVariables();

        private IList<Variable> ComputeFreeVariables()
        {
            var vars = new List<Variable>();
            foreach (var terminal in Parameters)
            {
                vars.AddRange(terminal.Variables);
            }

            return vars;
        }
    }

    public class UnaryFormula : Formula
    {
        public UnaryConnective Connective { get; set; }
        public Formula Formula { get; set; }

        public override IList<Variable> FreeVariables => Formula.FreeVariables;
    }

    public class BinaryFormula : Formula
    {
        public BinaryConnective Connective { get; set; }
        public Formula LHSFormula { get; set; }
        public Formula RHSFormula { get; set; }

        public override IList<Variable> FreeVariables => ComputeFreeVariables();

        private IList<Variable> ComputeFreeVariables()
        {
            var vars = new List<Variable>();
            vars.AddRange(LHSFormula.FreeVariables);
            vars.AddRange(RHSFormula.FreeVariables);
            return vars;
        }
    }

    public class QuantifierFormula : Formula
    {
        public Quantifier Quantifier { get; set; }
        public Variable Variable { get; set; }
        public Formula Formula { get; set; }

        public override IList<Variable> FreeVariables => ComputeFreeVariables();

        private IList<Variable> ComputeFreeVariables()
        {
            var vars = new List<Variable>();
            vars.AddRange(Formula.FreeVariables);
            vars.Remove(Variable);
            return vars;
        }
    }
}
