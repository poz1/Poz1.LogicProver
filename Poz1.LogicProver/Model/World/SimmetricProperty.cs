using Poz1.LogicProver.Model.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poz1.LogicProver.Model.World
{
    public class SimmetricProperty : IRelationProperty
    {
        public void AddRelation(AccessibilityRelation relation, WorldSymbol x, WorldSymbol y)
        {
            relation.AddRelation(y, x);
        }

        public Substitution<WorldSymbol> WorldUnify(AccessibilityRelation relation, WorldIndex i, WorldIndex j)
        {
            return null;
        }
    }
}
