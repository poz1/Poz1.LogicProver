using Poz1.LogicProver.Model.World;
using System.Collections.Generic;
using System.Text;

namespace Poz1.LogicProver.Model.Core
{
    public class QuantifierFormula : Formula
    {
        public string Quantifier { get; set; }
        public VariableTerminal Variable { get; set; }
        public Formula Formula { get; set; }

        public override List<VariableTerminal> FreeVariables => ComputeFreeVariables();

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
    }
}
