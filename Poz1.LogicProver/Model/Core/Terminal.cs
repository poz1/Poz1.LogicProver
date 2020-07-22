using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poz1.LogicProver.Model.Core
{
    public abstract class Terminal 
    {
        public Terminal(string value) 
        {
            Value = value;
        }

        public string Value { get; set; }
        public abstract List<VariableTerminal> Variables { get; }
    }

    public class ConstantTerminal : Terminal
    {
        public ConstantTerminal(string value) : base(value)
        {
        }

        public override List<VariableTerminal> Variables { get => new List<VariableTerminal>(); }
        public override string ToString()
        {
            return Value;
        }
    }

    public class VariableTerminal : Terminal
    {
        public VariableTerminal(string value) : base(value)
        {
        }

        public override List<VariableTerminal> Variables { get => new List<VariableTerminal>() { this }; }
        public override string ToString()
        {
            return Value;
        }
    }

    public class FunctionTerminal : Terminal
    {
        public List<Terminal> Parameters { get; set; }
        public int Arity => Parameters.Count();

        public override List<VariableTerminal> Variables { get => ComputeVariables();}

        public FunctionTerminal(string value, IList<Terminal> parameters) : base(value)
        {
            Parameters = new List<Terminal>(parameters);
        }

        private List<VariableTerminal> ComputeVariables()
        {
            var vars = new List<VariableTerminal>();
            foreach (var terminal in Parameters)
            {
                vars.AddRange(terminal.Variables);
            }
            return vars;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(Value);
            stringBuilder.Append('(');

            for(int i = 0; i < Parameters.Count; i++)
            {
                stringBuilder.Append(Parameters[i]);
                
                if(i != Parameters.Count - 1)
                    stringBuilder.Append(',');
            }

            stringBuilder.Append(')');
            return stringBuilder.ToString();
        }
    }
}