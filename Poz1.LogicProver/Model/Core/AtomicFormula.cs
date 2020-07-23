using Poz1.LogicProver.Model.World;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poz1.LogicProver.Model.Core
{
    public class AtomicFormula : Formula
    {
        public Terminal Terminal { get; }
        public int Arity => Terminal is FunctionTerminal functionTerminal ? functionTerminal.Parameters.Count : 0;

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
                    vars.AddRange(terminal.Variables);
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
    }
}
