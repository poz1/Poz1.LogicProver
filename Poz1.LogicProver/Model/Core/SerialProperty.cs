using Poz1.LogicProver.Model.Core;
using Poz1.LogicProver.Model.MGU;
using System.Collections.Generic;
using System.Linq;

namespace Poz1.LogicProver.Model.Core
{
    public class SerialProperty : IRelationProperty
    {
        public void AddRelation(AccessibilityRelation relation, WorldSymbol x, WorldSymbol y)
        {
        }

        public Substitution WorldUnify(AccessibilityRelation relation, WorldIndex i, WorldIndex j)
        {
            //2b -> one is ground and the other world variable
            if (i.EndSymbol.IsGround || j.EndSymbol.IsGround)
            {
                //Need to verify if n is accessible from the parent of w
                var n = i.EndSymbol.IsGround ? i.EndSymbol : j.EndSymbol;
                var w = !i.EndSymbol.IsGround ? i.EndSymbol : j.EndSymbol;

                if (relation.Contains(w.ParentSymbol, n)) {
                    var sub = new Substitution();
                    sub.Add(n, w);
                    return sub;
                }

                return null;
            }
            //2c -> both are world variables
            else
            {
                //(i)
                if (relation.Contains(i.EndSymbol.ParentSymbol, j.EndSymbol) || relation.Contains(j.EndSymbol.ParentSymbol, i.EndSymbol))
                    return new Substitution(  i.EndSymbol, j.EndSymbol  );
                //(ii)
                else
                {
                    var parentUnification = relation.WorldUnify(i.ParentIndex, j.ParentIndex);
                    if (parentUnification == null)
                        return null;
                    else
                    {
                        new Substitution( i.EndSymbol, j.EndSymbol ).Compose(parentUnification);
                        return parentUnification;
                    }
                }
            }
        }
    }
}
