﻿using Poz1.LogicProver.Model.Core;
using System.Collections.Generic;
using System.ComponentModel;

namespace Poz1.LogicProver.Model.Core
{
    public class AccessibilityRelation
    {
        internal Dictionary<WorldSymbol, List<WorldSymbol>> Relations { get; }

        protected readonly List<IRelationProperty> properties = new List<IRelationProperty>();

        public WorldSymbol BaseWorld { get; set; }

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

        public Substitution WorldUnify(WorldIndex i, WorldIndex j)
        {
            //By convention "0" is the actual world
            if (i.StartSymbol.Equals(BaseWorld) || j.StartSymbol.Equals(BaseWorld))
                return null;

            if (i.EndSymbol.IsGround && j.EndSymbol.IsGround)
            {
                if (i.EndSymbol == j.EndSymbol)
                    return new Substitution();
                else
                    return null;
            }

            //return AbstractWorldUnify(i, j);

            var unifications = new List<Substitution>();
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
            foreach (var symbol in worldIndex.Symbols)
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
            if (x == null || y == null)
                return false;

            return Relations[x].Contains(y);
        }
    }
}
