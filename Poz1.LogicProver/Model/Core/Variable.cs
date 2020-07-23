using System;
using System.Collections.Generic;
using System.Text;

namespace Poz1.LogicProver.Model.Core
{
    public abstract class LogicElement { }

    public class Constant: LogicElement
    {
        public string Name { get; }

        public Constant(string name)
        {
            Name = name;
        }
    }

    public class Variable: LogicElement
    {
        public string Name { get; }

        public Variable(string name)
        {
            Name = name;
        }
    }

    public class Function<T>: LogicElement
    {
        public string Name { get; }
        public List<T> Parameters = new List<T>();
        public Function(string name, IList<T> parameters)
        {
            Name = name;
            Parameters.AddRange(parameters);
        }
    }
}
