using System;
using System.Collections.Generic;
using System.Text;

namespace Poz1.LogicProver.Model.Core
{
    public abstract class LogicElement {
        public string Name { get; }

        public LogicElement(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
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

    public class Function<T>: LogicElement
    {
        public List<T> Parameters = new List<T>();
        public int Arity => Parameters.Count;

        public Function(string name, IList<T> parameters) :base(name)
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
