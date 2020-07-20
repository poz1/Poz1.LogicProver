using Poz1.LogicProver.Model.Core;
using System.Collections.Generic;

namespace Poz1.LogicProver.Model.World
{
    public interface IRelationProperty
    {
        Substitution<Terminal> WorldUnify(AccessibilityRelation relation, WorldIndex i, WorldIndex j);
        void AddRelation(AccessibilityRelation relation, WorldSymbol x, WorldSymbol y);
    }
}