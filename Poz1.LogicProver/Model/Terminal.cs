using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace Poz1.LogicProver.Model
{
    public abstract class Terminal
    {
        public abstract IList<Variable> Variables { get; }
    }

    public class Variable : Terminal
    {
        public string Value { get; set; }
        public override IList<Variable> Variables { get => new List<Variable>() { this }; }
    }

    public class Constant : Terminal
    {
        public string Value { get; set; }

        public override IList<Variable> Variables { get => new List<Variable>(); }
    }

    public class Function : Terminal
    {
        public string Value { get; set; }
        public IEnumerable<Terminal> Parameters { get; set; }
        public int Arity => Variables.Count();

        public override IList<Variable> Variables { get => ComputeVariables();}

        private IList<Variable> ComputeVariables()
        {
            var vars = new List<Variable>();
            foreach (var terminal in Parameters)
            {
                vars.AddRange(terminal.Variables);
            }

            return vars;
        }
    }
}