using Poz1.LogicProver.Model.MGU;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Poz1.LogicProver.Model.Core
{
    public class Substitution 
    {
        Dictionary<LogicElement, LogicElement> Elements;

        public Substitution()
        {
            Elements = new Dictionary<LogicElement, LogicElement>();
        }

        public Substitution(params ILogicElement[] elements) : this()
        {
            if (elements.Length % 2 != 0)
                throw new Exception("We need an even count of symbols");

            for (int i = 0; i < elements.Length; i += 2)
            {
                Add(elements[i], elements[i + 1]);
            }
        }

        public Substitution(IList<Equation<LogicElement>> equations) : this()
        {
            foreach (var equation in equations)
            {
                Add(equation.Terminal1, equation.Terminal2);
            }
        }

        public List<LogicElement> Domain { get => Elements.Keys.ToList(); }
        public List<LogicElement> Range { get => Elements.Values.ToList(); }

        public LogicElement GetValue(LogicElement item)
        {
            return Elements[item];
        }

        public bool IsPure { get => !Range.Any(x => Domain.Contains(x)); }

        //Apply

        public void Add(ILogicElement x, ILogicElement y)
        {
            Add(x.ToLogicElement(), y.ToLogicElement());
        }

        public void Add(LogicElement x, LogicElement y)
        {
            Elements.Add(x, y);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        internal void Compose(Substitution substitutions)
        {
            foreach(var item in substitutions.Elements.Keys)
            {
                if (Elements.ContainsKey(item))
                {
                    Elements[item] = substitutions.Elements[item];
                } 
                else
                {
                    Add(item, substitutions.Elements[item]);
                }
            }
        }
    }
}