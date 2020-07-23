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

                if (relation.Contains(w.ParentSymbol, n))
                    return new Substitution() {{ n, w } };
                return null;

                //var possibleUnifications = relation.Relations.Where(x => x.Value.Contains(n) && x.Value.Contains(w)).Select(x => x.Key);

                //var MGU = new MostGeneralUnifier();

                //foreach (var pos in possibleUnifications)
                //{
                //    MGU.AddEquation(new VariableTerminal() { Value = w.ParentSymbol.Symbol }, new ConstantTerminal() { Value = pos.Symbol });

                //    var unification = MGU.Unify(new List<Equation<Terminal>>()
                //    {
                //        new Equation<Terminal>(new VariableTerminal() { Value = w.ParentSymbol.Symbol }, new ConstantTerminal() { Value = pos.Symbol })
                //    });

                //    if (unification != null)
                //    {
                //        //Need to merge subs
                //        //var result = new Substitution<WorldSymbol>() { { n, w } }.Compose(unification);
                //        return null;
                //    }
                //    MGU.Clear();
                //    //Must be true
                //    //if (relation.Contains(s[0].Value, n))
                //    //    return new Substitution<WorldSymbol>() { { n, w } };

                //}
            }
            //2c -> both are world variables
            else
            {
                //(i)
                if (relation.Contains(i.EndSymbol.ParentSymbol, j.EndSymbol) || relation.Contains(j.EndSymbol.ParentSymbol, i.EndSymbol))
                    return new Substitution( { i.EndSymbol, j.EndSymbol } );
                //(ii)
                else
                {
                    var parentUnification = relation.WorldUnify(i.ParentIndex, j.ParentIndex);
                    if (parentUnification == null)
                        return null;
                    else
                    {
                        new Substitution() { { i.EndSymbol, j.EndSymbol } }.Compose(parentUnification);
                        return parentUnification;
                    }
                }
            }
        }
    }
}
