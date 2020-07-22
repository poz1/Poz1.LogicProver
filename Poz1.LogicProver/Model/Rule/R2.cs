using Poz1.LogicProver.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poz1.LogicProver.Model.Rule
{
    public class R2 : IInferenceRule
    {
        // R2: If S, |(p->q)|i <- T then S, |q|i <-|p|i, T
        public Sequent Apply(Sequent sequent)
        {
            BinaryFormula implicationFormula = (BinaryFormula)sequent.LeftHandSide.Formulas.Where(
                x => x is BinaryFormula formula && formula.Connective == BinaryConnective.Implication
                ).FirstOrDefault();
            
            if(implicationFormula != null)
            {
                sequent.LeftHandSide.Formulas.Remove(implicationFormula);

                var result = new Sequent();

                result.LeftHandSide.Formulas.AddRange(sequent.LeftHandSide.Formulas);
                result.LeftHandSide.Formulas.Add(implicationFormula.RHSFormula);

                result.RightHandSide.Formulas.AddRange(sequent.RightHandSide.Formulas);
                result.RightHandSide.Formulas.Add(implicationFormula.LHSFormula);

                return result;
            }

            return null;
        }
    }
}
