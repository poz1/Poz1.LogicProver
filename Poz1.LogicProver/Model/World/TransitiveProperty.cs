using Poz1.LogicProver.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poz1.LogicProver.Model.World
{
    public class TransitiveProperty : IRelationProperty
    {
        public void AddRelation(AccessibilityRelation relation, WorldSymbol x, WorldSymbol y)
        {
            if (x == y)
                return;

            var relations = relation.Relations.Where(rel => rel.Key == y);

            foreach(var rel in relations)
            {
                foreach(var target in rel.Value)
                    relation.AddRelation(x, target);
            }

            relations = relation.Relations.Where(rel => rel.Value.Contains(x));
            foreach (var rel in relations)
            {
                relation.AddRelation(rel.Key, y);
            }
        }

        public Substitution<Terminal> WorldUnify(AccessibilityRelation relation, WorldIndex i, WorldIndex j)
        {
            return null;
        }
    }
}
