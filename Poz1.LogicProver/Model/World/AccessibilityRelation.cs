using Poz1.LogicProver.Model.Core;
using System.Collections.Generic;
using System.ComponentModel;

namespace Poz1.LogicProver.Model.World
{
    public class AccessibilityRelation
    {
        internal Dictionary<WorldSymbol, List<WorldSymbol>> Relations { get; }

        protected readonly List<IRelationProperty> properties = new List<IRelationProperty>();

        public AccessibilityRelation() {
            Relations = new Dictionary<WorldSymbol, List<WorldSymbol>>();
        }

        public AccessibilityRelation(List<IRelationProperty> properties = null, List<WorldIndex> worlds = null) : this()
        {
            if (properties != null)
                this.properties = properties;

            if (worlds != null)
                foreach (var worldIndex in worlds)
                    AddWorldIndex(worldIndex);
        }

        public AccessibilityRelation(List<WorldIndex> worlds) : this()
        {
            foreach (var worldIndex in worlds)
                AddWorldIndex(worldIndex);
        }

        public Substitution<WorldSymbol> WorldUnify(WorldIndex i, WorldIndex j)
        {
            //By convention "0" is the actual world
            if (i.StartSymbol.Symbol != "0" || j.StartSymbol.Symbol != "0")
                return null;

            if (i.EndSymbol.IsGround && j.EndSymbol.IsGround)
            {
                if (i.EndSymbol.Symbol == j.EndSymbol.Symbol)
                    return new Substitution<WorldSymbol>();
                else
                    return null;
            }

            //return AbstractWorldUnify(i, j);

            var unifications = new List<Substitution<WorldSymbol>>();
            foreach (var property in properties)
            {
                var unification = property.WorldUnify(this, i, j);
                if (unification != null)
                    unifications.Add(unification);
            }

            if (unifications.Count == 0)
                return null;
            else
                return unifications[0];
        }

        public void AddWorldIndex(WorldIndex worldIndex)
        {
            foreach (var symbol in worldIndex)
            {
                if (symbol.ParentSymbol != null)
                {
                    AddRelation(symbol.ParentSymbol, symbol);
                }
            }
        }

        internal void AddRelation(WorldSymbol x, WorldSymbol y)
        {
            if (!Relations.ContainsKey(x))
                Relations.Add(x, new List<WorldSymbol>());

            if (!Relations[x].Contains(y))
            {
                Relations[x].Add(y);

                foreach (var property in properties)
                {
                    property.AddRelation(this, x,y);
                }
            }
        }

        public bool Contains(WorldSymbol x, WorldSymbol y)
        {
            return Relations[x].Contains(y);
        }

        //protected abstract Substitution<WorldSymbol> AbstractWorldUnify(WorldIndex i, WorldIndex j);
    }
}
