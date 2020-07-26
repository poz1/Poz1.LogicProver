using Poz1.LogicProver.Model.Core;
using System.Collections.Generic;

namespace Poz1.LogicProver.Model.Rule
{
    // R1: If S,|p|i <- T and S' <- |q|j, T' and |p|i and |q|j m-unify with unification σ then Sσ U S'σ <- Tσ U T'σ
    public class R1 : IResolutionRule
    {
        readonly AccessibilityRelation relation;

        public R1(AccessibilityRelation relation)
        {
            this.relation = relation;
        }

        public List<Sequent> Apply(Sequent leftS, Sequent rightS)
        { 
            return Apply(leftS, rightS, true);
        }


        public List<Sequent> Apply(Sequent leftS, Sequent rightS, bool recurse = true)
        {
            leftS = leftS.Clone();
            rightS = rightS.Clone();

            var ps = leftS.LeftHandSide.Formulas;
            var qs = rightS.RightHandSide.Formulas;

            //if ((leftS.LeftHandSide.Count == 0 && leftS.RightHandSide.Count != 0) &&
            //    (rightS.RightHandSide.Count == 0 && rightS.LeftHandSide.Count != 0))
            //{
            //    ps = rightS.LeftHandSide.Formulas;
            //    qs = leftS.RightHandSide.Formulas;
            //}

            foreach (var formulaP in ps)
            {
                relation.AddWorldIndex(formulaP.WorldIndex);
                foreach (var formulaQ in qs)
                {
                    relation.AddWorldIndex(formulaQ.WorldIndex);

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

                    result.LeftHandSide.Formulas.Remove(formulaP);
                    result.RightHandSide.Formulas.Remove(formulaQ);

                    result.Name = "R1 (" + leftS.Name + ", " + rightS.Name + ") with " + unification.ToString();
                    return new List<Sequent>() { result };
                }
            }

            if (recurse)
                return Apply(rightS, leftS, false);

            return null;
        }
    }
}
