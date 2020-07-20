using System.Collections.Generic;
using System.Linq;

namespace Poz1.LogicProver.Model.Core
{
    public abstract class Terminal 
    {
        public string Value { get; set; }
        public abstract List<VariableTerminal> Variables { get; }
    }

    public class ConstantTerminal : Terminal
    {
        public override List<VariableTerminal> Variables { get => new List<VariableTerminal>(); }
    }

    public class VariableTerminal : Terminal
    {
        public override List<VariableTerminal> Variables { get => new List<VariableTerminal>() { this }; }
    }

    public class FunctionTerminal : Terminal
    {
        public List<Terminal> Parameters { get; set; }
        public int Arity => Parameters.Count();

        public override List<VariableTerminal> Variables { get => ComputeVariables();}

        private List<VariableTerminal> ComputeVariables()
        {
            var vars = new List<VariableTerminal>();
            foreach (var terminal in Parameters)
            {
                vars.AddRange(terminal.Variables);
            }
            return vars;
        }
    }
}