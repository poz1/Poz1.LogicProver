using Poz1.LogicProver.Model.MGU;
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

        public Substitution(List<Equation<LogicElement>> equations)
        {
            foreach(var eq in equations)
            {
                Elements.Add(eq.Terminal1, eq.Terminal2);
            }
        }

        public Substitution(Equation<LogicElement> eq)
        {
            Elements.Add(eq.Terminal1, eq.Terminal2);
        }

        public List<LogicElement> Domain { get => Elements.Keys.ToList(); }
        public List<LogicElement> Range { get => Elements.Values.ToList(); }

        public bool IsPure { get => !Range.Any(x => Domain.Contains(x)); }

        //Apply

        public void Add(WorldSymbol x, WorldSymbol y)
        {
            Add(x.BaseElement, y.BaseElement);
        }

        public void Add(Terminal x, Terminal y)
        {
            Add(x.BaseElement, y.BaseElement);
        }

        private void Add(LogicElement x, LogicElement y)
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