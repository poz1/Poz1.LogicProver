﻿using System.Collections.Generic;
using System.Text;

namespace Poz1.LogicProver.Model.Core
{
    public class QuantifierFormula : Formula
    {
        public string Quantifier { get; set; }
        public VariableTerminal Variable { get; set; }
        public Formula Formula { get; set; }

        public override List<VariableTerminal> FreeVariables => ComputeFreeVariables();
        public override List<Terminal> Variables => Formula.Variables;

        public QuantifierFormula(Formula formula, VariableTerminal variable, string quantifier, WorldIndex index) : base(index)
        {
            Formula = formula;
            Variable = variable;
            Quantifier = quantifier;
        }

        private List<VariableTerminal> ComputeFreeVariables()
        {
            var vars = new List<VariableTerminal>();
            vars.AddRange(Formula.FreeVariables);
            vars.Remove(Variable);
            return vars;
        }

        public override void ApplySubstitution(Substitution substitution)
        {
            Formula.ApplySubstitution(substitution);
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append('(');
            stringBuilder.Append(Quantifier);
            stringBuilder.Append(Variable);
            stringBuilder.Append(')');

            stringBuilder.Append(Formula.ToString());

            return stringBuilder.ToString();
        }

        public override void ChangeWorldIndex(WorldIndex value)
        {
            WorldIndex = value;
            Formula.ChangeWorldIndex(value);
        }

        public override Formula Clone()
        {
            var clone = (QuantifierFormula)MemberwiseClone();
            clone.Formula = Formula.Clone();
            clone.WorldIndex = WorldIndex.Clone();
            clone.ChangeWorldIndex(clone.WorldIndex);
            return clone;
        }

        internal override Formula Simplify()
        {
            Formula = Formula.Simplify();

            return Quantifier switch
            {
                // ∃(x)p = ~ ∀(x) ~ p
                QuantifierConnective.Exist => new UnaryFormula(
                                                    new QuantifierFormula(
                                                          new UnaryFormula(Formula, UnaryConnective.Negation, Formula.WorldIndex),
                                                    Variable, QuantifierConnective.ForAll, WorldIndex),
                                               UnaryConnective.Negation, WorldIndex),
                _ => this,
            };
        }
    }
}
