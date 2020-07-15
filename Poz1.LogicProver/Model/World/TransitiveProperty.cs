using Poz1.LogicProver.Model.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poz1.LogicProver.Model.World
{
    public class TransitiveProperty : IRelationProperty
    {
        public void AddRelation(AccessibilityRelation relation, WorldSymbol x, WorldSymbol y)
        {
            foreach(var rel in relation.Relations)
            {
                if (rel.Value.Contains(x))
                {
                    relation.AddRelation(rel.Key, y);
                }
            }
        }

        public Substitution<WorldSymbol> WorldUnify(AccessibilityRelation relation, WorldIndex i, WorldIndex j)
        {
            return null;
        }
    }
}
