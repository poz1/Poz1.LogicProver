using Poz1.LogicProver.Model.Core;
using System.Collections.Generic;

namespace Poz1.LogicProver.Model.World
{
    public class ReflexiveProperty : IRelationProperty
    {
        public void AddRelation(AccessibilityRelation relation, WorldSymbol x, WorldSymbol y)
        {
            relation.AddRelation(x, x);
            relation.AddRelation(y, y);
        }

        public Substitution<Terminal> WorldUnify(AccessibilityRelation relations, WorldIndex i, WorldIndex j)
        {
            return null;
        }
    }
}
