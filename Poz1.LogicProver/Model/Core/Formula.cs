using Poz1.LogicProver.Model.MGU;
using Poz1.LogicProver.Model.World;
using System;
using System.Collections.Generic;

namespace Poz1.LogicProver.Model.Core
{
    public abstract class Formula 
    {    
        public WorldIndex WorldIndex { get; set; }

        public abstract List<VariableTerminal> FreeVariables { get; }

        public Substitution<Terminal> Unify(Formula formula)
        {
            var mgu = new MostGeneralUnifier();

            var itemsCount = FreeVariables.Count < formula.FreeVariables.Count ? FreeVariables.Count : formula.FreeVariables.Count;

            for (int i = 0; i < itemsCount; i++)
            {
                mgu.AddEquation(FreeVariables[i], formula.FreeVariables[i]);
            }

            return mgu.Compute();
        }

        public Substitution<Terminal> MUnify(AccessibilityRelation relation, Formula formula)
        {
            var unif = Unify(formula);
            var eta = relation.WorldUnify(WorldIndex, formula.WorldIndex);

            unif.Compose(eta);
            return unif;
        }

        public void ApplySubstitution(Substitution<Terminal> substitution) { }
    }

    public class AtomicFormula : Formula
    {
        public string Predicate { get; set; }
        public List<Terminal> Parameters { get; set; }
        public int Arity => Parameters.Count;

        public override List<VariableTerminal> FreeVariables => ComputeFreeVariables();

        //public override Substitution<Terminal> MUnify(Formula formula)
        //{
        //    var mgu = new MostGeneralUnifier();

        //    var itemsCount = FreeVariables.Count < formula.FreeVariables.Count ? FreeVariables.Count : formula.FreeVariables.Count;

        //    for(int i = 0; i< itemsCount; i++)
        //    {
        //        mgu.AddEquation(FreeVariables[i], formula.FreeVariables[i]);
        //    }

        //    return mgu.Compute();
        //}

        private List<VariableTerminal> ComputeFreeVariables()
        {
            var vars = new List<VariableTerminal>();
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

        public override List<VariableTerminal> FreeVariables => Formula.FreeVariables;

        //public override Substitution<Terminal> MUnify(Formula formula)
        //{
        //    throw new NotImplementedException();
        //}
    }

    public class BinaryFormula : Formula
    {
        public BinaryConnective Connective { get; set; }
        public Formula LHSFormula { get; set; }
        public Formula RHSFormula { get; set; }

        public override List<VariableTerminal> FreeVariables => ComputeFreeVariables();

        //public override Substitution<Terminal> MUnify(Formula formula)
        //{
        //    throw new NotImplementedException();
        //}

        private List<VariableTerminal> ComputeFreeVariables()
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

        public override List<VariableTerminal> FreeVariables => ComputeFreeVariables();

        private List<VariableTerminal> ComputeFreeVariables()
        {
            var vars = new List<VariableTerminal>();
            vars.AddRange(Formula.FreeVariables);
            vars.Remove(Variable);
            return vars;
        }
    }
}
