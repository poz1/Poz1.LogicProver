using System.Collections.Generic;
using System.Text;

namespace Poz1.LogicProver.Model.Core
{
    public class UnaryFormula : Formula
    {
        public string Connective { get; set; }

        private Formula formula;
        public Formula Formula
        {
            get => formula;
            set
            {
                formula = value;
                formula.WorldIndex = WorldIndex;
            }
        }

        public override List<VariableTerminal> FreeVariables => Formula.FreeVariables;

        public UnaryFormula(Formula formula, string connective, WorldIndex index) : base(index)
        {
            Formula = formula;
            Connective = connective;
        }

        public override void ApplySubstitution(Substitution substitution)
        {
            Formula.ApplySubstitution(substitution);
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(Connective);
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
            var clone = (UnaryFormula)MemberwiseClone();
            clone.Formula = Formula.Clone();
            clone.WorldIndex = WorldIndex.Clone();
            clone.ChangeWorldIndex(clone.WorldIndex);
            return clone;
        }
    }
}
