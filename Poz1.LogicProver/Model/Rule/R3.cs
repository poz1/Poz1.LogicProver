using Poz1.LogicProver.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poz1.LogicProver.Model.Rule
{
    public class R3 : IInferenceRule
    {
        public Sequent Apply(Sequent sequent)
        {
            BinaryFormula implicationFormula = (BinaryFormula)sequent.RightHandSide.Formulas.Where(
                x => x is BinaryFormula formula && formula.Connective == BinaryConnective.Implication
                ).FirstOrDefault();

            if (implicationFormula == null)
                return null;

            sequent.RightHandSide.Formulas.Remove(implicationFormula);



            sequent.RightHandSide.Formulas.Add(implicationFormula.RHSFormula);

                return sequent;

        }
    }
}
