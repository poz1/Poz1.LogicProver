using System.Collections.Generic;

namespace Poz1.LogicProver.Model.Core
{
    public class AtomicFormula : Formula
    {
        public Terminal Terminal { get; }
        public int Arity => Terminal is FunctionTerminal functionTerminal ? functionTerminal.Arity : 0;

        public override List<VariableTerminal> FreeVariables => ComputeFreeVariables();

        public AtomicFormula(Terminal terminal, WorldIndex index) : base(index)
        {
            Terminal = terminal;
        }

        public AtomicFormula(Terminal terminal)
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
                    vars.AddRange(terminal.FreeVariables);
                }
            }

            return vars;
        }

        public override string ToString()
        {
            return Terminal.ToString();
        }

        public override void ChangeWorldIndex(WorldIndex value)
        {
            WorldIndex = value;
        }

        public override Formula Clone()
        {
            var clone = (Formula)MemberwiseClone();
            clone.WorldIndex = WorldIndex.Clone();
            clone.ChangeWorldIndex(clone.WorldIndex);
            return clone;
        }

        public override void ApplySubstitution(Substitution substitution)
        {
            foreach(var item in substitution.Domain)
            {
                if(!(Terminal is FunctionTerminal) && Terminal.BaseElement.Name == item.Name)
                    Terminal.BaseElement = substitution.GetValue(item);

                else if (Terminal is FunctionTerminal function)
                {
                    function.BaseElement.Substitute(item, substitution.GetValue(item));
                }
            }
        }
    }
}
