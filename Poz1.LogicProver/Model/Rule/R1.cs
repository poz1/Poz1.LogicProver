using Poz1.LogicProver.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Poz1.LogicProver.Model.Rule
{
    // R1: If S,|p|i <- T and S' <- |q|j, T' and |p|i and |q|j m-unify with unification σ then Sσ U S'σ <- Tσ U T'σ
    public class R1 : IResolutionRule
    {
        AccessibilityRelation relation;

        public R1(AccessibilityRelation relation)
        {
            this.relation = relation;
        }

        public List<Sequent> Apply(Sequent leftS, Sequent rightS)
        {
            //var baseWorld = new WorldSymbol("0");
            //var wi1 = new WorldIndex(new WorldSymbol("f(w)", new WorldSymbol("w", baseWorld)));
            //var wi2 = new WorldIndex(new WorldSymbol("w1", baseWorld));

            //var relation = new AccessibilityRelation(
            //   new List<IRelationProperty>()
            //   {
            //        new SerialProperty()
            //   }
            //   //,
            //   //new List<WorldIndex>() { wi1, wi2 }
            // );


            var ps = leftS.LeftHandSide.Formulas;
            var qs = rightS.RightHandSide.Formulas;

            foreach(var formulaP in ps)
            {
                foreach (var formulaQ in qs)
                {
                    var unification = formulaP.MUnify(relation, formulaQ);
                    if (unification == null)
                        continue;

                    leftS.LeftHandSide.Formulas.ForEach(x => x.ApplySubstitution(unification));
                    rightS.LeftHandSide.Formulas.ForEach(x => x.ApplySubstitution(unification));

                    leftS.RightHandSide.Formulas.ForEach(x => x.ApplySubstitution(unification));
                    rightS.RightHandSide.Formulas.ForEach(x => x.ApplySubstitution(unification));

                    var result = new Sequent();

                    result.LeftHandSide.Formulas.AddRange(leftS.LeftHandSide.Formulas);
                    result.LeftHandSide.Formulas.AddRange(rightS.LeftHandSide.Formulas);

                    result.RightHandSide.Formulas.AddRange(leftS.RightHandSide.Formulas);
                    result.RightHandSide.Formulas.AddRange(rightS.RightHandSide.Formulas);

                    return new List<Sequent>() { result };
                }
            }

            return null;
        }
    }
}
