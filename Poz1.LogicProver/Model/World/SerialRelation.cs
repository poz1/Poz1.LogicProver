using Poz1.LogicProver.Model.Core;
using System;
using System.Collections.Generic;

namespace Poz1.LogicProver.Model.World
{
    public class SerialRelation : AccessibilityRelation
    {
        public override void Add(WorldSymbol x, WorldSymbol y)
        {
            if (y == null)
                throw new Exception("Relation is serial, y cannot be null");

            if (!relations.ContainsKey(x))
                relations.Add(x, new List<WorldSymbol>());

            relations[x].Add(y);
        }

        protected override Substitution<WorldSymbol> AbstractWorldUnify(WorldIndex i, WorldIndex j)
        {
            //2b -> one is ground and the other world variable
            if (i.EndSymbol.IsGround || j.EndSymbol.IsGround)
            {
                //Need to verify if n is accessible from the parent of w
                var n = i.EndSymbol.IsGround ? i.EndSymbol : j.EndSymbol;
                var w = !i.EndSymbol.IsGround ? i.EndSymbol : j.EndSymbol;

                if (Belongs(w.ParentSymbol, n))
                    return new Substitution<WorldSymbol>(){ { n, w } };
                else 
                    return null;
            }
            //2c -> both are world variables
            else
            {
                //(i)
                if (Belongs(i.EndSymbol.ParentSymbol, j.EndSymbol) || Belongs(j.EndSymbol.ParentSymbol, i.EndSymbol))
                    return new Substitution<WorldSymbol>() { { i.EndSymbol, j.EndSymbol } };
                //(ii)
                else
                {
                    var parentUnification = WorldUnify(i.ParentIndex, j.ParentIndex);
                    if (parentUnification == null)
                        return null;
                    else
                    {
                        parentUnification.Compose(new Substitution<WorldSymbol>() { { i.EndSymbol, j.EndSymbol } });
                        return parentUnification;
                    }
                }
            }
        }
    }
}
