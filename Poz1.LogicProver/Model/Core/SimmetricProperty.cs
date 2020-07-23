using Poz1.LogicProver.Model.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poz1.LogicProver.Model.Core
{
    public class SimmetricProperty : IRelationProperty
    {
        public void AddRelation(AccessibilityRelation relation, WorldSymbol x, WorldSymbol y)
        {
            relation.AddRelation(y, x);
        }

        public Substitution WorldUnify(AccessibilityRelation relation, WorldIndex i, WorldIndex j)
        {
            return null;
        }
    }
}
