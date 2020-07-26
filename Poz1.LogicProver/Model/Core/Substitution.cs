using Poz1.LogicProver.Model.MGU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Poz1.LogicProver.Model.Core
{
    public class Substitution 
    {
        Dictionary<ILogicElement, ILogicElement> Elements;

        public Substitution()
        {
            Elements = new Dictionary<ILogicElement, ILogicElement>();
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

        public Substitution(IList<Equation<ILogicElement>> equations) : this()
        {
            foreach (var equation in equations)
            {
                Add(equation.Terminal1, equation.Terminal2);
            }
        }

        public List<ILogicElement> Domain { get => Elements.Keys.ToList(); }
        public List<ILogicElement> Range { get => Elements.Values.ToList(); }

        public ILogicElement GetValue(ILogicElement item)
        {
            return Elements[item];
        }

        public bool IsPure { get => !Range.Any(x => Domain.Contains(x)); }

        public void Add(ILogicElement x, ILogicElement y)
        {
            Elements.Add(x, y);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("{");

            var keys = Elements.Keys;
            var vals = Elements.Values;

            for (int i = 0; i < Elements.Count; i++)
            {
                sb.Append(keys.ElementAt(i) + "/" + vals.ElementAt(i));
                if(i != Elements.Count - 1)
                    sb.Append(", ");
            }

            sb.Append("}");
            return sb.ToString();
        }

        internal void Compose(Substitution substitutions)
        {
            if(substitutions != null)
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