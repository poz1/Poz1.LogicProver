using System;
using System.Collections.Generic;
using System.Text;

namespace Poz1.LogicProver.Model.Core
{
    public abstract class LogicElement : ILogicElement {
        public string Name { get; protected set; }

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

        public abstract void Substitute(LogicElement item, LogicElement logicElement);
    }

    public class Constant: LogicElement
    {
        public Constant(string name) : base(name) { }

        public override void Substitute(LogicElement item, LogicElement logicElement)
        {
            if (Name == item.Name)
                Name = logicElement.Name;
        }
    }

    public class Variable: LogicElement
    {
        public Variable(string name) : base(name) { }

        public override void Substitute(LogicElement item, LogicElement logicElement)
        {
            if (Name == item.Name)
                Name = logicElement.Name;
        }
    }

    public class Function<T>: LogicElement
    {
        public List<T> Parameters = new List<T>();
        public int Arity => Parameters.Count;

        public Function(string name, IList<T> parameters) : base(name)
        {
            Parameters.AddRange(parameters);
        }

        public override void Substitute(LogicElement item, LogicElement logicElement)
        {
            if (Name == item.Name)
                Name = logicElement.Name;

            foreach (var param in Parameters)
            {
                if (param is LogicElement element)
                    element.Substitute(item, logicElement);
                else if (param is ILogicElement logicBox)
                    logicBox.ToLogicElement().Substitute(item, logicElement);
            }
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
