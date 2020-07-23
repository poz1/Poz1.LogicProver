using System;
using System.Collections.Generic;
using System.Text;

namespace Poz1.LogicProver.Model.Core
{
    public abstract class LogicElement : ILogicElement {
        public string Name { get; }

        public LogicElement(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        public LogicElement ToLogicElement()
        {
            return this;
        }
    }

    public class Constant: LogicElement
    {
        public Constant(string name) : base(name) { }
    }

    public class Variable: LogicElement
    {
        public Variable(string name) : base(name) { }
    }

    public class Function: LogicElement
    {
        public List<LogicElement> Parameters = new List<LogicElement>();
        public int Arity => Parameters.Count;

        public Function(string name, List<ILogicElement> parameters) :base(name)
        {
            parameters.ForEach(x => Parameters.Add(x.ToLogicElement()));   
        }

        public Function(string name, IList<LogicElement> parameters) : base(name)
        {
            Parameters.AddRange(parameters);
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(Name);
            stringBuilder.Append('(');

            for (int i = 0; i < Parameters.Count; i++)
            {
                stringBuilder.Append(Parameters[i]);

                if (i != Parameters.Count - 1)
                    stringBuilder.Append(',');
            }

            stringBuilder.Append(')');
            return stringBuilder.ToString();
        }
    }
}
