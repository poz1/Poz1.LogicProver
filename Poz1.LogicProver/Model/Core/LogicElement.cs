﻿using System;
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
    }

    public class Constant: LogicElement
    {
        public Constant(string name) : base(name) { }
    }

    public class Variable: LogicElement
    {
        public Variable(string name) : base(name) { }
    }

    public class Function<T>: LogicElement where T:ILogicElement
    {
        public List<T> Parameters = new List<T>();
        public int Arity => Parameters.Count;

        public Function(string name, IList<T> parameters) : base(name)
        {
            Parameters.AddRange(parameters);
        }

        public void Substitute(ILogicElement item, ILogicElement logicElement)
        {
            var newParams = new List<T>(Parameters);

            foreach (var param in Parameters)
            {
                LogicElement originalParam = null;

                if (param is ILogicElement originalParamBox)
                    originalParam = originalParamBox.ToLogicElement();

                else if (param is LogicElement originalPar)
                    originalParam = originalPar;

                if (originalParam.Name == item.ToLogicElement().Name)
                    newParams[Parameters.IndexOf(param)] = (T)logicElement;

                Parameters = newParams;
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
