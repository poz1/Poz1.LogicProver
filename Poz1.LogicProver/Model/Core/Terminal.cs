using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poz1.LogicProver.Model.Core
{
    public abstract class Terminal<T> 
    {
        public Terminal(T baseItem) 
        {
            BaseItem = baseItem;
        }

        public T BaseItem { protected get; set; }
        public abstract List<VariableTerminal> FreeVariables { get; }
    }

    public class ConstantTerminal : Terminal<Constant>
    {
        public ConstantTerminal(string value) : base(new Constant(value))
        {
        }

        public override List<VariableTerminal> FreeVariables { get => new List<VariableTerminal>(); }
        public override string ToString()
        {
            return BaseItem.Name;
        }
    }

    public class VariableTerminal : Terminal<Variable>
    {
        public VariableTerminal(string value) : base(new Variable(value))
        {
        }

        public override List<VariableTerminal> FreeVariables { get => new List<VariableTerminal>() { this }; }
        public override string ToString()
        {
            return BaseItem.Name;
        }
    }

    public class FunctionTerminal : Terminal<Function<Terminal<LogicElement>>>
    {
        public int Arity => BaseItem.Parameters.Count();

        public override List<VariableTerminal> FreeVariables { get => ComputeVariables();}

        public FunctionTerminal(string value, IList<Terminal<LogicElement>> parameters) : 
            base(new Function<Terminal<LogicElement>>(value, parameters))
        {
        }

        private List<VariableTerminal> ComputeVariables()
        {
            var vars = new List<VariableTerminal>();
            foreach (var terminal in BaseItem.Parameters)
            {
                vars.AddRange(terminal.FreeVariables);
            }
            return vars;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(BaseItem);
            stringBuilder.Append('(');

            for(int i = 0; i < BaseItem.Parameters.Count; i++)
            {
                stringBuilder.Append(BaseItem.Parameters[i]);
                
                if(i != BaseItem.Parameters.Count - 1)
                    stringBuilder.Append(',');
            }

            stringBuilder.Append(')');
            return stringBuilder.ToString();
        }
    }
}