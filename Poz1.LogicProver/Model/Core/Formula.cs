using Poz1.LogicProver.Model.MGU;
using Poz1.LogicProver.Model.World;
using System.Collections.Generic;
using System.Text;

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

        public Formula(WorldIndex index)
        {
            WorldIndex = index;
        }
    }

    public class AtomicFormula : Formula
    {
        public Terminal Terminal { get;}
        public int Arity => Terminal is FunctionTerminal functionTerminal ? functionTerminal.Parameters.Count : 0;

        public override List<VariableTerminal> FreeVariables => ComputeFreeVariables();

        public AtomicFormula(Terminal terminal, WorldIndex index) : base(index)
        {
            Terminal = terminal;
        }

        private List<VariableTerminal> ComputeFreeVariables()
        {
            var vars = new List<VariableTerminal>();

            if (Terminal is FunctionTerminal functionTerminal)
            {
                foreach (var terminal in functionTerminal.Parameters)
                {
                    vars.AddRange(terminal.Variables);
                }
            }

            return vars;
        }

        public override string ToString()
        {
            return Terminal.ToString();
        }
    }

    public class UnaryFormula : Formula
    {
        public string Connective { get; set; }
        public Formula Formula { get; set; }

        public override List<VariableTerminal> FreeVariables => Formula.FreeVariables;

        public UnaryFormula(Formula formula, string connective, WorldIndex index) : base(index)
        {
            Formula = formula;
            Connective = connective;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

                stringBuilder.Append(Connective);
                stringBuilder.Append(Formula.ToString());
            
            return stringBuilder.ToString();
        }
    }

    public class BinaryFormula : Formula
    {
        public string Connective { get; set; }
        public Formula LHSFormula { get; set; }
        public Formula RHSFormula { get; set; }

        public override List<VariableTerminal> FreeVariables => ComputeFreeVariables();

        public BinaryFormula(Formula leftFormula, Formula rightFormula, string connective, WorldIndex index) : base(index)
        {
            LHSFormula = leftFormula;
            RHSFormula = rightFormula;
            Connective = connective;
        }

        private List<VariableTerminal> ComputeFreeVariables()
        {
            var vars = new List<VariableTerminal>();
            vars.AddRange(LHSFormula.FreeVariables);
            vars.AddRange(RHSFormula.FreeVariables);
            return vars;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(LHSFormula.ToString());
            stringBuilder.Append(Connective);
            stringBuilder.Append(RHSFormula.ToString());


            return stringBuilder.ToString();
        }
    }

    public class QuantifierFormula : Formula
    {
        public string Quantifier { get; set; }
        public VariableTerminal Variable { get; set; }
        public Formula Formula { get; set; }

        public override List<VariableTerminal> FreeVariables => ComputeFreeVariables();

        public QuantifierFormula(Formula formula, string quantifier, WorldIndex index) : base(index)
        {
            Formula = formula;
            Quantifier = quantifier;
        }

        private List<VariableTerminal> ComputeFreeVariables()
        {
            var vars = new List<VariableTerminal>();
            vars.AddRange(Formula.FreeVariables);
            vars.Remove(Variable);
            return vars;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();


            stringBuilder.Append(Quantifier);
            stringBuilder.Append(Formula.ToString());

            return stringBuilder.ToString();
        }
    }
}
