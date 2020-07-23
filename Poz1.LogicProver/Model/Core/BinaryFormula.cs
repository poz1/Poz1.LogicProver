using Poz1.LogicProver.Model.World;
using System.Collections.Generic;
using System.Text;

namespace Poz1.LogicProver.Model.Core
{
    public class BinaryFormula : Formula
    {
        public string Connective { get; set; }

        private Formula lHSFormula;
        public Formula LHSFormula
        {
            get => lHSFormula; set
            {
                lHSFormula = value;
                lHSFormula.WorldIndex = WorldIndex;
            }
        }

        private Formula rHSFormula;
        public Formula RHSFormula
        {
            get => rHSFormula; set
            {
                rHSFormula = value;
                rHSFormula.WorldIndex = WorldIndex;
            }
        }

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

        public override void ChangeWorldIndex(WorldIndex value)
        {
            WorldIndex = value;
            LHSFormula.ChangeWorldIndex(value);
            RHSFormula.ChangeWorldIndex(value);
        }

        public override Formula Clone()
        {
            var clone = (BinaryFormula)MemberwiseClone();
            clone.LHSFormula = LHSFormula.Clone();
            clone.RHSFormula = RHSFormula.Clone();
            clone.WorldIndex = WorldIndex.Clone();
            clone.ChangeWorldIndex(clone.WorldIndex);
            return clone;
        }
    }
}
