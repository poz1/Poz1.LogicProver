using Poz1.LogicProver.Model.Core;
using System.Collections.Generic;

namespace Poz1.LogicProver.Model.Core
{
    public interface IRelationProperty
    {
        Substitution WorldUnify(AccessibilityRelation relation, WorldIndex i, WorldIndex j);
        void AddRelation(AccessibilityRelation relation, WorldSymbol x, WorldSymbol y);
    }
}