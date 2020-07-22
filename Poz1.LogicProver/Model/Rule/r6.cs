using Poz1.LogicProver.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poz1.LogicProver.Model.Rule
{
    class R6 : IInferenceRule
    {
        public Sequent Apply(Sequent sequent)
        {

            UnaryFormula implicationFormula = (UnaryFormula)sequent.RightHandSide.Formulas.Where(
                x => x is UnaryFormula formula && formula.Connective == UnaryConnective.Negation
                ).FirstOrDefault();

            if (implicationFormula != null)
            {
                sequent.LeftHandSide.Formulas.Remove(implicationFormula);

                var result = new Sequent();

                result.LeftHandSide.Formulas.AddRange(sequent.LeftHandSide.Formulas);
                result.LeftHandSide.Formulas.Add(implicationFormula.Formula);

                result.RightHandSide.Formulas.AddRange(sequent.RightHandSide.Formulas);

                return result;
            }

            return null;
        }
    }
}
