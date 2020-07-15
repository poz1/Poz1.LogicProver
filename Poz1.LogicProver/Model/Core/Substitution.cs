using Poz1.LogicProver.Model;
using Poz1.LogicProver.Model.MGU;
using Poz1.LogicProver.Model.World;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Poz1.LogicProver.Model.Core
{
    public class Substitution<T> : Dictionary<T, T>
    {
        public Substitution()
        {
        }

        public Substitution(List<Equation<T>> equations)
        {
            foreach(var eq in equations)
            {
                Add(eq.Terminal1, eq.Terminal2);
            }
        }

        public List<T> Domain { get => Keys.ToList(); }
        public List<T> Range { get => Values.ToList(); }

        public bool IsPure { get => !Range.Any(x => Domain.Contains(x)); }

        //Apply

        public override string ToString()
        {
            return base.ToString();
        }

        internal void Compose(Substitution<T> substitutions)
        {
            foreach(var item in substitutions.Keys)
            {
                if (ContainsKey(item))
                {
                    this[item] = substitutions[item];
                } 
                else
                {
                    Add(item, substitutions[item]);
                }
            }
        }
    }
}